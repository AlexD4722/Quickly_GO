using MediatR;
using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Queries;

namespace QuicklyGo.Feature.ChatHub.Handlers.Queries
{
    public class GetConversationIdsByMessageIdsHandler : IRequestHandler<GetConversationIdsByMessageIdsQuery, IEnumerable<string>>
    {
        public IGenericRepository<QuicklyGo.Models.Conversation> _conversationRepository;
        public GetConversationIdsByMessageIdsHandler(IGenericRepository<QuicklyGo.Models.Conversation> conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }
        public async Task<IEnumerable<string>> Handle(GetConversationIdsByMessageIdsQuery request, CancellationToken cancellationToken)
        {
            var conversationIds = await _conversationRepository.Query()
                .Where(c => c.Messages.Any(m => request.MessageIds.Contains(m.Id)))
                .Select(c => c.Id)
                .ToListAsync();
            return conversationIds;
        }
    }
}
