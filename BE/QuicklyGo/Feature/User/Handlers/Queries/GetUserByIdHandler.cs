using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.User.Requests.Queries;

namespace QuicklyGo.Feature.User.Handlers.Queries
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, QuicklyGo.Models.User?>
    {
        public IUserRepository _userRepository;
        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<QuicklyGo.Models.User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.Get(request.UserId);
        }
    }
}
