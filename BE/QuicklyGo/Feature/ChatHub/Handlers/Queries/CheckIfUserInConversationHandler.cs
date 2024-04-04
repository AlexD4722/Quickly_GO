using MediatR;
using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Queries;

namespace QuicklyGo.Feature.ChatHub.Handlers.Queries
{
    public class CheckIfUserInConversationHandler : IRequestHandler<CheckIfUserInConversationQuery, bool>
    {
        public IGenericRepository<QuicklyGo.Models.Conversation> _conversationRepository;
        public CheckIfUserInConversationHandler(IGenericRepository<QuicklyGo.Models.Conversation> conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }
        public async Task<bool> Handle(CheckIfUserInConversationQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
            {
                throw new ArgumentNullException("UserId cannot be found");
            }
            if (request.ConversationId == null)
            {
                throw new ArgumentNullException("ConversationId is null");
            }
            return await _conversationRepository
                .Query()
                .AsSplitQuery()
                .AnyAsync(c => c.Id == request.ConversationId && c.UserConversations.Any(uc => uc.UserId == request.UserId));
        }
    }
}
