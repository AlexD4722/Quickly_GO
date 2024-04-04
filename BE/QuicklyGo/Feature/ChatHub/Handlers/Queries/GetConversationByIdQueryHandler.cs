using MediatR;
using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Queries;

namespace QuicklyGo.Feature.ChatHub.Handlers.Queries
{
    public class GetConversationByIdQueryHandler : IRequestHandler<GetConversationByIdQuery, QuicklyGo.Models.Conversation>
    {
        public IConversationRepository _conversation;
        public GetConversationByIdQueryHandler(IConversationRepository conversation) 
        {
            _conversation = conversation;
        }
        public async Task<QuicklyGo.Models.Conversation?> Handle(GetConversationByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.IncludeUserConversations)
            {
                return await _conversation.Query()
                .Include(c => c.UserConversations)
                .Where(c => c.Id == request.ConversationId)
                .FirstOrDefaultAsync();
            }
            return await _conversation.FirstOrDefault(c => c.Id == request.ConversationId);
        }
    }
}
