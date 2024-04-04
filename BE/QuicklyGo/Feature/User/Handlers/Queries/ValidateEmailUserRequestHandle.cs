using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.User.Requests.Queries;

namespace QuicklyGo.Feature.User.Handlers.Queries
{
    public class ValidateEmailUserRequestHandle : IRequestHandler<ValidateEmailUserRequest, bool>
    {
        private readonly IUserRepository _userRepository;
        public ValidateEmailUserRequestHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(ValidateEmailUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByKeyUnique(request.UserId);
            if (user == null || user.Email == null || user.VerifyCode != request.CodeValidateEmail)
            {
                return false;
            }
            return true;
        }
    }
}
