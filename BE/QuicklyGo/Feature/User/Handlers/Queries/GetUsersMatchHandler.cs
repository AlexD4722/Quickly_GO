using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.User.Requests.Queries;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.User.Handlers.Queries
{
    public class GetUsersMatchHandler : IRequestHandler<GetUsersMatchRequest, List<QuicklyGo.Models.User>>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersMatchHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<QuicklyGo.Models.User>> Handle(GetUsersMatchRequest request, CancellationToken cancellationToken)
        {
            return await _userRepository.ToList(request.Predicate);
        }
    }
}
