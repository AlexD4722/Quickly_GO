using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Queries;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.ChatHub.Handlers.Queries
{
    public class GetRelationshipByIdHandler : IRequestHandler<GetRelationshipByIdQuery, Relationship>
    {
        private IGenericRepository<Relationship> _relationshipRepository { get; }
        public GetRelationshipByIdHandler(IGenericRepository<Relationship> relationshipRepository)
        {
            _relationshipRepository = relationshipRepository;
        }
        public async Task<Relationship> Handle(GetRelationshipByIdQuery request, CancellationToken cancellationToken)
        {
            return await _relationshipRepository.Get(request.RelationshipId);
        }
    }
}
