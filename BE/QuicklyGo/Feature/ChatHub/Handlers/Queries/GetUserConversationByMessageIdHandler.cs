using MediatR;
using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Queries;

namespace QuicklyGo.Feature.ChatHub.Handlers.Queries
{
    public class GetUserConversationByMessageIdHandler : IRequestHandler<GetUserConversationByMessageAndUserIdQuery, QuicklyGo.Models.UserConversation>
    {
        public IUnitOfWork _unitOfWork;
        public GetUserConversationByMessageIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<QuicklyGo.Models.UserConversation?> Handle(GetUserConversationByMessageAndUserIdQuery request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.MessageRepository
                .Query()
                .Include(m => m.Conversation)
                .ThenInclude(c => c.UserConversations)
                .FirstOrDefaultAsync(m => m.Id == request.MessageId);
            var conversation = message?.Conversation;
            return conversation?.UserConversations
                .FirstOrDefault(uc => uc.UserId == request.UserId);
        }
    }
}
