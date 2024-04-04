using MediatR;
using QuicklyGo.Data.DTOs.Conversation;

namespace QuicklyGo.Feature.Conversation.Requests.Command
{
    public class UpdateConversationRequest : IRequest<ConversationInfoDTO>
    {
        public ConversationInfoDTO _conversationInfoDTO;

        public UpdateConversationRequest(ConversationInfoDTO conversationInfoDTO)
        {
            _conversationInfoDTO = conversationInfoDTO;
        }
    }
}
