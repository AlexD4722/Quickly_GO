using MediatR;
using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Queries;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.ChatHub.Handlers.Queries
{
    public class CheckIfUserInDeclinedHandler : IRequestHandler<CheckIfUserInDeclinedQuery, bool>
    {
        public IGenericRepository<QuicklyGo.Models.Relationship> _relationshipRepository;
        public CheckIfUserInDeclinedHandler(IGenericRepository<QuicklyGo.Models.Relationship> relationshipRepository)
        {
            _relationshipRepository = relationshipRepository;
        }
        public async Task<bool> Handle(CheckIfUserInDeclinedQuery request, CancellationToken cancellationToken)
        {
            return await _relationshipRepository.Query()
                .AnyAsync(r => r.UserId == request.ResponserId && r.FriendId == request.RequesterId && r.Status == RelationshipStatus.Declined);
        }
    }
}
