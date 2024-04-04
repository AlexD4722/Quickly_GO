using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Commands
{
    public class AddNewMessageCommand : IRequest
    {
        public QuicklyGo.Models.Message Message;
        public AddNewMessageCommand(QuicklyGo.Models.Message message)
        {
            Message = message;
        }
    }
}
