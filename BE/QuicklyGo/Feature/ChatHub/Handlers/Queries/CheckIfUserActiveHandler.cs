using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Queries;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.ChatHub.Handlers.Queries
{
    public class CheckIfUserActiveHandler : IRequestHandler<CheckIfUserActiveQuery, bool>
    {
        public IGenericRepository<QuicklyGo.Models.User> _userRepository;
        public CheckIfUserActiveHandler(IGenericRepository<QuicklyGo.Models.User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(CheckIfUserActiveQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.UserId);
            return user.Status == UserStatus.Active;
        }
    }
}
