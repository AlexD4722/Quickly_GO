using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Commands;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.ChatHub.Handlers.Commands
{
    public class SetRelationshipStatusHandler : IRequestHandler<SetRelationshipStatusCommand, MediatR.Unit>
    {
        public IGenericRepository<Relationship> _relationshipRepository;
        public SetRelationshipStatusHandler(IGenericRepository<Relationship> relationshipRepository)
        {
            _relationshipRepository = relationshipRepository;
        }
        public async Task<MediatR.Unit> Handle(SetRelationshipStatusCommand request, CancellationToken cancellationToken)
        {
            var relationship = await _relationshipRepository.Get(request.RelationshipId);
            relationship.Status = request.Status;
            await _relationshipRepository.Update(relationship);
            await _relationshipRepository.Save();
            return MediatR.Unit.Value;
        }
    } 
}
