using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Commands
{
    public class AddNewMessagesCommand : IRequest<MediatR.Unit>
    {
        public List<QuicklyGo.Models.Message> Messages { get; set; }
        public AddNewMessagesCommand(List<QuicklyGo.Models.Message> messages)
        {
            Messages = messages;
        }
    }
}
