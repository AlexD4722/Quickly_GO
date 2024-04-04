using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Commands
{
    public class AddReadMessagesCommand : IRequest<MediatR.Unit>
    {
        public List<int> ReadMessages { get; set; }
        public string UserId { get; set; }
        public AddReadMessagesCommand(List<int> readMessages, string userId)
        {
            ReadMessages = readMessages;
            UserId = userId;
        }
    }
}
