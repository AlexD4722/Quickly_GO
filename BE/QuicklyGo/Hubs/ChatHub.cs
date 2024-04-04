using AutoMapper;
using Azure;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.AspNetCore.SignalR;
using QuicklyGo.Data.DTOs.ChatHub;
using QuicklyGo.Data.DTOs.Conversation;
using QuicklyGo.Data.DTOs.Message;
using QuicklyGo.Data.DTOs.ReadMessage;
using QuicklyGo.Data.DTOs.User;
using QuicklyGo.Feature.ChatHub.Requests.Commands;
using QuicklyGo.Feature.ChatHub.Requests.Queries;
using QuicklyGo.Feature.User.Requests.Command;
using QuicklyGo.Feature.User.Requests.Queries;
using QuicklyGo.Models;
using QuicklyGo.Utils;
using System.Security.Claims;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace QuicklyGo.Hubs
{
    [Authorize]
    public class ChatHub : Hub<ChatClientSide>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public ChatHub(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            // Add the userId to the groups of the conversations that the user is a member of
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var conversations = await _mediator.Send(new GetConversationsByUserIdQuery(userId));
            foreach (var conversation in conversations)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, conversation.Id);
            }

            var responseConversation = conversations
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.UrlImg,
                    c.Description,
                    c.Group,
                    c.CreateAt,
                    c.UpdateAt,
                    User = c.UserConversations
                .Select(uc => new QuicklyGo.Models.User
                {
                    Id = uc.User.Id,
                    UserName = uc.User.UserName,
                    LastName = uc.User.LastName,
                    FirstName = uc.User.FirstName,
                    UrlImgAvatar = uc.User.UrlImgAvatar,
                    UrlBackground = uc.User.UrlBackground,
                    Status = uc.User.Status,
                })
                .Where(u => u.Id != userId)
                .ToList(),
                    LastMessage = c.Messages
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    CreatorId = m.CreatorId,
                    BodyContent = m.BodyContent,
                }).LastOrDefault(),
                    LastReadMessage = c.UserConversations.FirstOrDefault(uc => uc.UserId == userId)?.LastSeenMessage
                });

            // Get the relationships of the user
            var relationships = await _mediator.Send(new GetRelationshipsByUserIdQuery(userId));

            // Send all information of the user to the client
            await Clients.Caller.Connected(new { conversations = responseConversation, relationships });
        }

        public async Task<object> HandleMessage(MessageDto newMessage)
        {
            try
            {
                // Check type of the conversation and if it is a group conversation,
                // check if the user is a member of the group

                newMessage.CreatorId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var conversation = await _mediator
                    .Send(new GetConversationByIdQuery(newMessage.ConversationId, true));
                if (conversation == null)
                {
                    return new { message = "Conversation not found", success = false };
                }
                var foundInGroup = false;
                foreach (var uc in conversation.UserConversations)
                {
                    if (uc.UserId == newMessage.CreatorId)
                    {
                        foundInGroup = true;
                        break;
                    }
                }
                if (!foundInGroup)
                {
                    return new { message = "User is not in the conversation", success = false };
                }

                // Write the message to the database

                var message = _mapper.Map<Message>(newMessage);
                await _mediator.Send(new AddNewMessageCommand(message));

                // Send the conversation refresh notify to the all conversation members

                await Clients.OthersInGroup(conversation.Id)
                    .RefreshConversation(new { conversationId = conversation.Id });

                // Form the response
                var messages = await _mediator
                    .Send(new GetMessagesByConversationIdQuery(conversation.Id, 20));

                return new
                {
                    message = "Message sent",
                    success = true,
                    conversation = new
                    {
                        conversationId = conversation.Id,
                        messages
                    }
                };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message, success = false, trace = ex.StackTrace };
            }
        }

        public async Task<object> UpdateMessage(UpdateMessageDto newMessage)
        {
            try
            {
                // Check if the user is the creator of the message
                var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var messageCreatorId = await _mediator
                    .Send(new GetUserIdByMessageIdQuery(newMessage.Id));
                if (messageCreatorId != userId)
                {
                    return new { message = "User is not the creator of the message", success = false };
                }

                // Update the message in the database
                var updatedMessage = _mapper.Map<Message>(newMessage);
                await _mediator.Send(new UpdateMessageCommand(updatedMessage));

                // Send the conversation refresh notify to the all conversation members
                await Clients.OthersInGroup(newMessage.ConversationId)
                    .RefreshConversation(new { conversationId = newMessage.ConversationId });

                return new { message = "Message updated", success = true };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message, success = false, trace = ex.StackTrace };
            }
        }

        public async Task<object> GetMessageInConversation(string conversationId)
        {
            try
            {
                // check if the user is a member of the conversation
                var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var isUserInConversation = await _mediator
                    .Send(new CheckIfUserInConversationQuery(userId, conversationId));
                if (!isUserInConversation)
                {
                    return new { message = "User is not in the conversation", success = false };
                }

                // Get the messages of the conversation
                var messages = await _mediator
                    .Send(new GetMessagesByConversationIdQuery(conversationId, 20));
                return new { conversation = new { conversationId, messages }, success = true };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message, success = false, trace = ex.StackTrace };
            }

        }

        public async Task<object> GetConversationData(string conversationId)
        {
            try
            {
                // check if the user is a member of the conversation
                var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var isUserInConversation = await _mediator
                    .Send(new CheckIfUserInConversationQuery(userId, conversationId));
                if (!isUserInConversation)
                {
                    return new { message = "User is not in the conversation", success = false };
                }

                // Get the conversation data
                var conversation = await _mediator
                    .Send(new GetConversationByIdQuery(conversationId));
                return new { conversation, success = true };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message, success = false, trace = ex.StackTrace };
            }
        }

        public async Task<object> GetUserData(string userId)
        {
            try
            {
                // check if the user has relation with the caller
                var callerId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var isUserInRelation = await _mediator
                    .Send(new CheckIfUserInPendingQuery(callerId, userId));
                if (!isUserInRelation)
                {
                    return new { message = "User is not in the relation", success = false };
                }

                // Get the user data
                var user = await _mediator
                    .Send(new GetUserByIdQuery(userId));
                return new { user = _mapper.Map<UserDto>(user), success = true };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message, success = false, trace = ex.StackTrace };
            }
        }

        public async Task<object> LookForUser(string keyword)
        {
            try
            {
                var callerId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var users = await _mediator
                    .Send(new GetUsersByKeywordQuery(keyword, callerId));
                return new { users, success = true };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message, success = false, trace = ex.StackTrace };
            }
        }

        public async Task<object> CreateConversation(CreateConversationDto newConversation)
        {
            try
            {
                // Check if the conversation info is valid
                if ((newConversation.Group) && (newConversation.Name == null || newConversation.Name.Length == 0))
                {
                    return new { message = "Group conversation name cannot be empty", success = false };
                }

                if (newConversation.Group)
                {
                    if (newConversation.UserIds.Count <= 1)
                    {
                        return new { message = "Group conversation must have at least 3 members", success = false };
                    }
                }
                else
                {
                    if (newConversation.UserIds.Count != 1)
                    {
                        return new { message = "Private conversation must have 2 members", success = false };
                    }
                }

                // Write the conversation to the database
                var callerId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var conversation = await _mediator.Send(new OpenNewConversationCommand(callerId, newConversation));

                // Add caller to the conversation group
                await Groups.AddToGroupAsync(Context.ConnectionId, conversation.Id);

                // Send the conversation refresh notify to the all conversation members

                foreach (var member in conversation.UserConversations)
                {
                    var memberConversations = await _mediator.Send(new GetConversationsByUserIdQuery(member.UserId));
                    var responseConversation = memberConversations
                    .Select(c => new
                    {
                        c.Id,
                        c.Name,
                        c.UrlImg,
                        c.Description,
                        c.Group,
                        c.CreateAt,
                        c.UpdateAt,
                        User = c.UserConversations
                    .Select(uc => new QuicklyGo.Models.User
                    {
                        Id = uc.User.Id,
                        UserName = uc.User.UserName,
                        LastName = uc.User.LastName,
                        FirstName = uc.User.FirstName,
                        UrlImgAvatar = uc.User.UrlImgAvatar,
                        UrlBackground = uc.User.UrlBackground,
                        Status = uc.User.Status,
                    })
                    .Where(u => u.Id != member.UserId)
                    .ToList(),
                        LastMessage = c.Messages
                    .Select(m => new MessageDto
                    {
                        Id = m.Id,
                        CreatorId = m.CreatorId,
                        BodyContent = m.BodyContent,
                    }).LastOrDefault(),
                        LastReadMessage = c.UserConversations.FirstOrDefault(uc => uc.UserId == member.UserId)?.LastSeenMessage
                    });
                    await Clients.User(member.UserId).RefreshChats(new { conversations = responseConversation });
                }


                return new { message = "Conversation created", success = true, conversationId = conversation.Id };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message, success = false, trace = ex.StackTrace };
            }
        }

        public async Task<object> MessageReadNotify(NotifyReadMessageDto inform)
        {
            try
            {
                // Check if messageIds are valid
                if (inform.MessageIds.Count == 0)
                {
                    return new { message = "MessageIds is empty", success = false };
                }

                if (inform.MessageIds.Count > 20)
                {
                    return new { message = "MessageIds is too long", success = false };
                }


                // Check if the user is a member of the conversation
                var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Write the read message to the database
                await _mediator.Send(new AddReadMessagesCommand(inform.MessageIds, userId));

                // Send the conversation refresh notify to the all conversation members

                var conversationIds = await _mediator.Send(new GetConversationIdsByMessageIdsQuery(inform.MessageIds));
                foreach (var conversationId in conversationIds)
                {
                    await Clients.OthersInGroup(conversationId)
                        .RefreshConversation(new { conversationId });
                }

                return new { message = "Message read", success = true };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message, success = false, trace = ex.StackTrace };
            }
        }

        public async Task<object> SendFriendRequest(string friendId)
        {
            try
            {
                // Check if the friendId is valid
                var callerId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (callerId == friendId)
                {
                    return new { message = "Can't add yourself as friend", success = false, isFriend = false };
                }

                var isUserActive = await _mediator
                     .Send(new CheckIfUserActiveQuery(friendId));
                if (!isUserActive)
                {
                    return new { Message = "User profile is deactivated", success = false };
                }

                // Check if the caller already has relation with the friend
                var isUserInPending = await _mediator
                    .Send(new CheckIfUserInPendingQuery(callerId, friendId));
                if (isUserInPending)
                {
                    return new { message = "Already sent friend request", success = false };
                }

                // Check if the friend has declined the caller
                var isUserInDeclined = await _mediator
                    .Send(new CheckIfUserInDeclinedQuery(callerId, friendId));
                var relationshipsFriends = new List<Relationship>();
                var relationships = new List<Relationship>();

                if (isUserInDeclined)
                {
                    // Set the relationship status to Pending and WatingForAccept
                    var listRelationships = await _mediator
                        .Send(new GetRelationshipsByUserIdsQuery(friendId, callerId));
                    var friendRelationship = listRelationships.Where(r => r.UserId == friendId).First();
                    var callerRelationship = listRelationships.Where(r => r.UserId == callerId).First();
                    await _mediator.Send(new SetRelationshipStatusCommand(callerRelationship.Id, RelationshipStatus.WaitingForAccept));
                    await _mediator.Send(new SetRelationshipStatusCommand(friendRelationship.Id, RelationshipStatus.Pending));
                    // Send the friend request to the friend
                    relationshipsFriends = (await _mediator.Send(new GetRelationshipsByUserIdQuery(friendId))).ToList();
                    await Clients.User(friendId)
                        .ReceiveFriendRequest(new { relationships = relationshipsFriends });

                    relationships = (await _mediator.Send(new GetRelationshipsByUserIdQuery(callerId))).ToList();
                    return new { message = "Friend request sent", success = true, dataRelationships = relationships };
                }
                var relationshipIds = await _mediator.Send(new AddRelationshipCommand(callerId, friendId));

                // Add new notice to the database
                var user = await _mediator.Send(new GetUserByIdQuery(callerId));
                var notice = new Notice
                {
                    Title = "New Friend request",
                    Description = $"You have a new friend request from {user.FirstName} {user.LastName}",
                    UserId = friendId,
                    Status = NoticeStatus.NotRead
                };
                notice = await _mediator.Send(new AddNewNoticeCommand(notice));



                // Send the friend request to the friend
                relationshipsFriends = (await _mediator.Send(new GetRelationshipsByUserIdQuery(friendId))).ToList();
                await Clients.User(friendId)
                    .ReceiveFriendRequest(new { relationships = relationshipsFriends, notice });

                // Send email to the friend
                var friend = await _mediator.Send(new GetUserByIdQuery(friendId));
                await MailSender.SendMailAsync(friend.Email, "New Friend Request", $"You have a new friend request from {user.FirstName} {user.LastName}");


                relationships = (await _mediator.Send(new GetRelationshipsByUserIdQuery(callerId))).ToList();
                return new { message = "Friend request sent", success = true, dataRelationships = relationships };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message, success = false, trace = ex.StackTrace };
            }
        }

        public async Task<object> ResponseFriendRequest(FriendRequestResponse response)
        {
            try
            {
                var callerId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var status = response.IsAccepted ? RelationshipStatus.Active : RelationshipStatus.Declined;

                // Get the relationship
                var relationships = await _mediator
                    .Send(new GetRelationshipsByUserIdsQuery(response.FriendId, callerId));

                // Check if the relationship is valid
                if (relationships.Count != 2)
                {
                    return new { message = "Relationships not found", success = false };
                }

                if (!relationships.Any(r => r.Status == RelationshipStatus.Pending))
                {
                    return new { message = "Relationship status is invalid", success = false };
                }

                // Set the relationship active to the database
                await _mediator.Send(new SetRelationshipStatusCommand(relationships[0].Id, status));
                await _mediator.Send(new SetRelationshipStatusCommand(relationships[1].Id, status));

                // Add new notice to the database
                var user = await _mediator.Send(new GetUserByIdQuery(callerId));
                var notice = new Notice
                {
                    Title = "Friend request response",
                    Description = $"{user.FirstName} {user.LastName} has responded to your friend request",
                    UserId = response.FriendId,
                    Status = NoticeStatus.NotRead
                };
                notice = await _mediator.Send(new AddNewNoticeCommand(notice));

                // Send the friend request response to the request-er
                await Clients.User(response.FriendId)
                    .ReceiveFriendRequestResponse(new { isAccepted = response.IsAccepted, relationship = relationships.Where(r => r.FriendId == response.FriendId).First(), notice });

                // Send email to the request-er
                var friend = await _mediator.Send(new GetUserByIdQuery(response.FriendId));
                await MailSender.SendMailAsync(friend.Email, "Friend Request Response", $"{user.FirstName} {user.LastName} has {(response.IsAccepted ? "accepted" : "declined")} to your friend request");

                // Get the relationships of the user
                var relationshipsNew = await _mediator.Send(new GetRelationshipsByUserIdQuery(callerId));
                //refresh the chat list of all members in the conversation of friend
                var relationshipsNewFriend = await _mediator.Send(new GetRelationshipsByUserIdQuery(response.FriendId));
                await Clients.User(response.FriendId).ReceiveFriendRequest(new { relationships = relationshipsNewFriend });
                // return the response for caller
                return new { message = "Friend request response sent", success = true, relationships = relationshipsNew };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message, success = false, trace = ex.StackTrace };
            }
        }
        public async Task<object> BlockUser(string userId)
        {
            try
            {
                var callerId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (callerId == userId)
                {
                    return new { message = "Can't block yourself", success = false };
                }

                // Get the relationship
                var relationships = await _mediator
                    .Send(new GetRelationshipsByUserIdsQuery(userId, callerId));
                if (relationships.Count != 2)
                {
                    var newRelationships = await _mediator.Send(new AddRelationshipCommand(callerId, userId));
                    await _mediator.Send(new SetRelationshipStatusCommand(newRelationships[0].Id, RelationshipStatus.Blocked));
                    await _mediator.Send(new SetRelationshipStatusCommand(newRelationships[1].Id, RelationshipStatus.Deleted));
                }
                else
                {
                    await _mediator.Send(new SetRelationshipStatusCommand(relationships.Where(r => r.UserId == callerId).First().Id, RelationshipStatus.Blocked));
                    await _mediator.Send(new SetRelationshipStatusCommand(relationships.Where(r => r.UserId == userId).First().Id, RelationshipStatus.Deleted));
                }

                return new { message = "User blocked", success = true };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message, success = false, trace = ex.StackTrace };
            }
        }
        public async Task<object> UpdateUserData(UpdateUserDto userDto)
        {
            try
            {
                // Check the user id
                var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId != userDto.Id)
                {
                    return new { message = "User id is invalid", success = false };
                }
                var userSearch = await _mediator.Send(new GetUserByIdQuery(userId));
                userSearch.LastName = userDto.LastName;
                userSearch.FirstName = userDto.FirstName;
                userSearch.Location = userDto.Location;
                userSearch.Description = userDto.Description;
                // get value of 
                var user = _mapper.Map<User>(userDto);
                //check userSearch is null
                if (userSearch != null)
                {
                    await _mediator.Send(new UpdateUserCommand(userSearch));
                }
                // Update user data 

                return new { message = "User updated", success = true };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message, success = false, trace = ex.StackTrace };
            }
        }

    }
}
