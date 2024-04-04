using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Commands
{
    public class UpdateMessageCommand : IRequest
    {
        public Models.Message Message { get; set; }
        public UpdateMessageCommand(Models.Message message)
        {
            Message = message;
        }
    }
}
