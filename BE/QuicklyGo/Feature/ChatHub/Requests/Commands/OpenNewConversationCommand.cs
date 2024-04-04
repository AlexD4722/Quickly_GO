using MediatR;
using QuicklyGo.Data.DTOs.Conversation;

namespace QuicklyGo.Feature.ChatHub.Requests.Commands
{
    public class OpenNewConversationCommand : IRequest<QuicklyGo.Models.Conversation>
    {
        public CreateConversationDto CreateConversationDto { get; set; }
        public string CallerId { get; set; }
        public OpenNewConversationCommand(string callerId, CreateConversationDto createConversationDto)
        {
            CreateConversationDto = createConversationDto;
            CallerId = callerId;
        }
    }
}
