using MediatR;

namespace QuicklyGo.Feature.Conversation.Requests.Command
{
    public class DeleteConversationRequest : IRequest<bool>
    {
        public string Id { get; set; }

        public DeleteConversationRequest(string id) { 
            Id = id;
        }
    }
}
