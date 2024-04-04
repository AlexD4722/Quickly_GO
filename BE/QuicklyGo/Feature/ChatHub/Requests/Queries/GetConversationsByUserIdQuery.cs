using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Queries
{
    public class GetConversationsByUserIdQuery : IRequest<List<QuicklyGo.Models.Conversation>>
    {
        public string UserId { get; set; }
        public GetConversationsByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
