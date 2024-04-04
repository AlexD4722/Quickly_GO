using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Queries;

namespace QuicklyGo.Feature.ChatHub.Handlers.Queries
{
    public class GetRelationshipsByUserIdsHandler : IRequestHandler<GetRelationshipsByUserIdsQuery, List<QuicklyGo.Models.Relationship>>
    {
        public IGenericRepository<QuicklyGo.Models.Relationship> _relationshipRepository;
        public GetRelationshipsByUserIdsHandler(IGenericRepository<QuicklyGo.Models.Relationship> relationshipRepository)
        {
            _relationshipRepository = relationshipRepository;
        }
        public async Task<List<QuicklyGo.Models.Relationship>> Handle(GetRelationshipsByUserIdsQuery request, CancellationToken cancellationToken)
        {
            return await _relationshipRepository
                .ToList(r => r.UserId == request.UserId1 && r.FriendId == request.UserId2 || r.UserId == request.UserId2 && r.FriendId == request.UserId1);
        }
    }
}
