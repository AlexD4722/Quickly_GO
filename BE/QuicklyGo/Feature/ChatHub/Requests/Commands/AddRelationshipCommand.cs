using MediatR;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.ChatHub.Requests.Commands
{
    public class AddRelationshipCommand : IRequest<List<Relationship>>
    {
        public string CallerId { get; set; }
        public string FriendId { get; set; }
        public AddRelationshipCommand(string callerId, string friendId)
        {
            CallerId = callerId;
            FriendId = friendId; 
        }
    }
}
