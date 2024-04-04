using MediatR;
using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Queries;

namespace QuicklyGo.Feature.ChatHub.Handlers.Queries
{
    public class GetUsersByKeywordHandler : IRequestHandler<GetUsersByKeywordQuery, List<Models.User>>
    {
        private readonly IGenericRepository<Models.User> _userRepository;
        public GetUsersByKeywordHandler(IGenericRepository<Models.User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<Models.User>> Handle(GetUsersByKeywordQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.Query()
                .Where(u => (!u.Relationships.Any(r => r.FriendId == request.CallerId && r.Status == Models.RelationshipStatus.Blocked)) && u.Id != request.CallerId)
                .Where(u => u.Status == Models.UserStatus.Active && u.Role == Models.Roles.User)
                .Select(u => new
                {
                    User = u,
                    FullName = u.FirstName + " " + u.LastName
                })
                .Where(u => u.FullName.Contains(request.Keyword) || u.User.PhoneNumber.Contains(request.Keyword))
                .Select(u => new Models.User
                {
                    Id = u.User.Id,
                    FirstName = u.User.FirstName,
                    LastName = u.User.LastName,
                    PhoneNumber = u.User.PhoneNumber,
                    UrlImgAvatar = u.User.UrlImgAvatar,
                    UrlBackground = u.User.UrlBackground,
                    Status = u.User.Status
                })
                .Take(5)
                .ToListAsync();
        }
    }
}
