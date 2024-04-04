using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Queries
{
    public class CheckIfUserInConversationQuery : IRequest<bool>
    {
        public string UserId { get; set; }
        public string ConversationId { get; set; }
        public CheckIfUserInConversationQuery(string userId, string conversationId)
        {
            UserId = userId;
            ConversationId = conversationId;
        }
    }
}
