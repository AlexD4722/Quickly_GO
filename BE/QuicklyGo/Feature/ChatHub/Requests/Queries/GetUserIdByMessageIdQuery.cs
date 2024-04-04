using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Queries
{
    public class GetUserIdByMessageIdQuery : IRequest<string>
    {
        public int MessageId { get; set; }
        public GetUserIdByMessageIdQuery(int messageId)
        {
            MessageId = messageId;
        }
    }
}
