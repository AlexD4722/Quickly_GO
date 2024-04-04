using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Queries
{
    public class GetConversationIdsByMessageIdsQuery : IRequest<IEnumerable<string>>
    {
        public List<int> MessageIds { get; set; }
        public GetConversationIdsByMessageIdsQuery(List<int> messageIds)
        {
            MessageIds = messageIds;
        }
    }
}
