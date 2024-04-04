using MediatR;
using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Queries;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.ChatHub.Handlers.Queries
{
    public class GetListFriendHandler : IRequestHandler<GetListFriendRequests, IEnumerable<Relationship>>
    {
        public IGenericRepository<Relationship> _relationshipRepository;
        public GetListFriendHandler(IGenericRepository<Relationship> relationshipRepository)
        {
            _relationshipRepository = relationshipRepository;
        }
        public async Task<IEnumerable<Relationship>> Handle(GetListFriendRequests request, CancellationToken cancellationToken)
        {
            var relationships = await _relationshipRepository.Query()
                .Include(r => r.User)
                .Include(r => r.Friend)
                .Where(r => (r.UserId == request.UserId ) && r.Status == RelationshipStatus.Pending)
                .ToListAsync();
            var result = relationships.Select(r => new Relationship
            {
                Id = r.Id,
                UserId = r.UserId,
                FriendId = r.FriendId,
                Friend = new QuicklyGo.Models.User
                {
                    Id = r.Friend.Id,
                    UserName = r.Friend.UserName,
                    LastName = r.Friend.LastName,
                    FirstName = r.Friend.FirstName,
                    UrlImgAvatar = r.Friend.UrlImgAvatar,
                    UrlBackground = r.Friend.UrlBackground,
                    Status = r.Friend.Status,
                },
                Alias = r.Alias,
                Status = r.Status,
            });
            return result;
        }
    }
}
