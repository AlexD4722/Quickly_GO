using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Queries
{
    public class GetUserConversationByMessageAndUserIdQuery : IRequest<QuicklyGo.Models.UserConversation>
    {
        public int MessageId { get; set; }
        public string UserId { get; set; }
        public GetUserConversationByMessageAndUserIdQuery(int messageId, string userId)
        {
            MessageId = messageId;
            UserId = userId;
        }
    }
}
