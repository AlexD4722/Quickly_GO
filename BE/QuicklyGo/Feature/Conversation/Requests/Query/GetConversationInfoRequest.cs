using MediatR;
using QuicklyGo.Data.DTOs.Conversation;

namespace QuicklyGo.Feature.Conversation.Requests.Query
{
    public class GetConversationInfoRequest : IRequest<ConversationInfoDTO>
    {
        public string Id { get; set; }

        public GetConversationInfoRequest(string id)
        {
            Id = id;
        }

    }
}
