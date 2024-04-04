using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Queries
{
    public class GetConversationByIdQuery : IRequest<QuicklyGo.Models.Conversation>
    {
        public string ConversationId { get; set; }
        public bool IncludeUserConversations { get; set; }
        public GetConversationByIdQuery(string conversationId, bool includeUserConversations = false)
        {
            ConversationId = conversationId;
            IncludeUserConversations = includeUserConversations;
        }        
    }
}
