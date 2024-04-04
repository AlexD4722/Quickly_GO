using AutoMapper;
using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Data.Responsive;
using QuicklyGo.Feature.User.Requests.Queries;


namespace QuicklyGo.Feature.User.Handlers.Queries
{
    public class ConfirmUserLoginHandler : IRequestHandler<ConfirmUserLoginRequest, QuicklyGo.Models.User?>
    {
        private readonly IGenericRepository<QuicklyGo.Models.User> _userRepository;

        public ConfirmUserLoginHandler(IGenericRepository<QuicklyGo.Models.User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<QuicklyGo.Models.User?> Handle(ConfirmUserLoginRequest request, CancellationToken cancellationToken)
        {
            if (request?.LoginUserDto == null || _userRepository == null)
            {
                throw new ArgumentNullException(nameof(request), "Request or UserRepository cannot be null.");
            }

            var user = await _userRepository
                .FirstOrDefault(x => x.UserName == request.LoginUserDto.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.LoginUserDto.Password, user.Password))
            {
                return null;
            }
            return user;
        }
    }
}
