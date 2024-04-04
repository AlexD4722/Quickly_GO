using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Commands;
using QuicklyGo.Unit;

namespace QuicklyGo.Feature.ChatHub.Handlers.Commands
{
    public class OpenNewConversationCommandHandler : IRequestHandler<OpenNewConversationCommand, QuicklyGo.Models.Conversation>
    {
        public IUnitOfWork _unitOfWork;
        public OpenNewConversationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<QuicklyGo.Models.Conversation> Handle(OpenNewConversationCommand request, CancellationToken cancellationToken)
        {
            // Add new conversation to context
            var conversation = new QuicklyGo.Models.Conversation
            {
                Id = CreateGenerateUniqueKey.GenerateUniqueKey(),
                Name = request.CreateConversationDto.Name,
                Description = request.CreateConversationDto.Description,
                Group = request.CreateConversationDto.Group,
                UrlImg = "Img/avatar/ImgGroupDefault.png",
                Status = Models.ConversationStatus.Active,
                UserConversations = new List<QuicklyGo.Models.UserConversation>()
            };
            conversation.UserConversations
                .Add(new Models.UserConversation { UserId = request.CallerId, LastSeenMessage = DateTime.Now });
            foreach (var userId in request.CreateConversationDto.UserIds)
            {
                var user = await _unitOfWork.UserRepository.Get(userId);
                conversation.UserConversations
                    .Add(new Models.UserConversation { User = user, UserId = user.Id, LastSeenMessage = DateTime.Now.AddSeconds(-1)});
            }
            try
            {
                await _unitOfWork.ConversationRepository.Add(conversation);
                await _unitOfWork.Save();
                return conversation;
            } catch (DbUpdateException)
            {
                throw new Exception("Error while adding new conversation to the database");
            }      
        }
    }
}
