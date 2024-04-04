using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Commands;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.ChatHub.Handlers.Commands
{
    public class AddRelationshipHandler : IRequestHandler<AddRelationshipCommand, List<Relationship>>
    {
        public IGenericRepository<Relationship> _relationshipRepository;
        public AddRelationshipHandler(IGenericRepository<Relationship> relationshipRepository)
        {
            _relationshipRepository = relationshipRepository;
        }
        public async Task<List<Relationship>> Handle(AddRelationshipCommand request, CancellationToken cancellationToken)
        {
            var relationship1 = _relationshipRepository.Query()
                .Add(new Relationship
                {
                    UserId = request.CallerId,
                    FriendId = request.FriendId,
                    Status = RelationshipStatus.WaitingForAccept
                });
            var relationship2 = _relationshipRepository.Query()
                .Add(new Relationship
                {
                    UserId = request.FriendId,
                    FriendId = request.CallerId,
                    Status = RelationshipStatus.Pending
                });
            await _relationshipRepository.Save();
            return new List<Relationship>() { relationship1.Entity, relationship2.Entity};
        }
    }
}
