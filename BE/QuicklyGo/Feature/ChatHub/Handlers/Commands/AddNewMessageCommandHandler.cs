using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Commands;

namespace QuicklyGo.Feature.ChatHub.Handlers.Commands
{
    public class AddNewMessageCommandHandler : IRequestHandler<AddNewMessageCommand>
    {
        public IUnitOfWork _unitOfWork;
        public AddNewMessageCommandHandler(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public async Task<MediatR.Unit> Handle(AddNewMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.MessageRepository.Add(request.Message);
            var userConversation = await _unitOfWork.UserConversationRepository
                .FirstOrDefault(uc => uc.UserId == request.Message.CreatorId && uc.ConversationId == request.Message.ConversationId);
            userConversation.LastSeenMessage = DateTime.Now;
            await _unitOfWork.Save();
            return MediatR.Unit.Value;
        }
    }
}
