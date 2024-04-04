using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Queries
{
    public class GetMessagesByConversationIdQuery : IRequest<List<QuicklyGo.Models.Message>>
    {
        public string ConversationId { get; set; }
        public int NumberOfMessages { get; set; }
        public GetMessagesByConversationIdQuery(string conversationId, int numberOfMessages)
        {
            ConversationId = conversationId;
            NumberOfMessages = numberOfMessages;
        }
    }
}
