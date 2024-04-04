using MediatR;
using QuicklyGo.Reponses;

namespace QuicklyGo.Feature.ChatHub.Requests.Commands
{
    public class UpdateStatusRelationshipCommand : IRequest<BaseCommandResponse>
    {
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public UpdateStatusRelationshipCommand(string UserId, string FriendId)
        {
            this.UserId = UserId;
            this.FriendId = FriendId;
        }
    }
}
