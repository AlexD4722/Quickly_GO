using MediatR;
using QuicklyGo.Data.DTOs.Conversation;
using QuicklyGo.Reponses;

namespace QuicklyGo.Feature.Conversation.Requests.Command
{
    public class CreateConversationRequest : IRequest<BaseCommandResponse>
    {
        public ConversationInfoDTO ConversationInfo { get; set; }

        public CreateConversationRequest (ConversationInfoDTO conversation){
            ConversationInfo = conversation;
        }
    }
}
