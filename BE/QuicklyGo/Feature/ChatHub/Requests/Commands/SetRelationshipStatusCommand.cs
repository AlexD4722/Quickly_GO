using MediatR;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.ChatHub.Requests.Commands
{
    public class SetRelationshipStatusCommand : IRequest<MediatR.Unit>
    {
        public int RelationshipId { get; set; }
        public RelationshipStatus Status { get; set; }
        public SetRelationshipStatusCommand(int relationshipId, RelationshipStatus status)
        {
            RelationshipId = relationshipId;
            Status = status;
        }
    }
}
