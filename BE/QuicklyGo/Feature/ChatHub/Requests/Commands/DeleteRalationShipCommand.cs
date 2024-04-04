using MediatR;
using QuicklyGo.Reponses;

namespace QuicklyGo.Feature.ChatHub.Requests.Commands
{
    public class DeleteRalationShipCommand : IRequest<BaseCommandResponse>
    {
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public DeleteRalationShipCommand(string UserId, string FriendId)
        {
            this.UserId = UserId;
            this.FriendId = FriendId;
        }
    }
}
