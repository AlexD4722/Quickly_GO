using MediatR;
using Microsoft.EntityFrameworkCore;
using QuicklyGo.Data;
using QuicklyGo.Data.DTOs.UserConvesation;
using QuicklyGo.Feature.UserConversations.Requests.Queries;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.UserConversations.Handlers.Queries
{
    public class GetConversationChatDetailByUserIdHandler : IRequestHandler<GetConversationChatDetailByUserIdRequest, ResponseChatConversation>
    {
        protected readonly QuicklyGoDbContext _dbContext;
        public GetConversationChatDetailByUserIdHandler(QuicklyGoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ResponseChatConversation> Handle(GetConversationChatDetailByUserIdRequest request, CancellationToken cancellationToken)
        {
            var respose = new ResponseChatConversation();
            var ListChatConversations = new List<ChatConversationDetialDto>();
            var UserConversations = await _dbContext.UserConversations
                .Include(uc => uc.Conversation)
                .Include(uc => uc.User)
                .ThenInclude(u => u.Relationships)
                .Where(uc => uc.UserId == request.UserId).ToListAsync();
            if (UserConversations != null)
            {
                foreach (var item in UserConversations)
                {
                    var conversation = await _dbContext.Conversations.FirstOrDefaultAsync(c => c.Id == item.ConversationId);
                    var lastMessage = await _dbContext.Messages.FirstOrDefaultAsync(m => m.CreateAt == item.LastSeenMessage);
                    if (conversation != null)
                    {
                        if (conversation.Group)
                        {
                            var chatConversation = new ChatConversationDetialDto()
                            {
                                ConversationId = conversation.Id,
                                Name = conversation.Name,
                                UrlImg = conversation.UrlImg,
                                LastMessage = lastMessage?.BodyContent,
                                LastReatTime = item.LastSeenMessage,
                                Description = conversation.Description,
                                Group = conversation.Group,
                                Status = conversation.Status
                            };
                            ListChatConversations.Add(chatConversation);
                        }
                        else
                        {
                            var UserGuess = await _dbContext.UserConversations
                                .Include(uc => uc.User)
                                .FirstOrDefaultAsync(uc => uc.ConversationId == item.ConversationId && uc.UserId != request.UserId);

                            if (UserGuess != null && UserGuess.User != null)
                            {
                                var itemRelationship = await _dbContext.Relationships.FirstOrDefaultAsync(ur => ur.UserId == item.UserId && ur.FriendId == UserGuess.UserId);
                                var chatConversation = new ChatConversationDetialDto()
                                {
                                    ConversationId = conversation.Id,
                                    Name = itemRelationship?.Alias,
                                    UrlImg = UserGuess.User.UrlImgAvatar,
                                    LastMessage = lastMessage?.BodyContent,
                                    LastReatTime = item.LastSeenMessage,
                                    Description = conversation.Description,
                                    Group = conversation.Group,
                                    Status = conversation.Status
                                };
                                ListChatConversations.Add(chatConversation);
                            }
                            else
                            {
                                respose.msg = "UserGuess is null";
                                return respose;
                            }
                        }
                    }
                    else
                    {
                        respose.msg = "conversation emty ";
                        return respose;
                    }
                }
                respose.msg = "return data";
                respose.data = ListChatConversations;
                return respose;
            }
            respose.msg = "UserConversations is null";
            return respose;
        }
    }
    public class ResponseChatConversation
    {
        public string msg { get; set; }
        public List<ChatConversationDetialDto> data { get; set; }
        public ResponseChatConversation()
        {
            msg = string.Empty;
            data = new List<ChatConversationDetialDto>();
        }
    }
}

