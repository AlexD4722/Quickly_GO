using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.User.Requests.Queries;
using QuicklyGo.Reponses;

namespace QuicklyGo.Feature.User.Handlers.Queries
{
    public class CheckCreatingUserInfoHandler : IRequestHandler<CheckCreatingUserInfoRequest, CheckCreatingUserInfoResponse>
    {
        private readonly IUserRepository _userRepository;
        public CheckCreatingUserInfoHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<CheckCreatingUserInfoResponse> Handle(CheckCreatingUserInfoRequest request, CancellationToken cancellationToken)
        {
            var nameMatchedUser = await _userRepository
                .FirstOrDefault(u => u.UserName == request.User.UserName);
            var emailMatchUser = await _userRepository
                .FirstOrDefault(u => u.Email == request.User.Email);
            var phoneMatchUser = await _userRepository
                .FirstOrDefault(u => u.PhoneNumber == request.User.PhoneNumber);

            var result = new CheckCreatingUserInfoResponse { EmailExists = false, PhoneNumberExists = false, UsernameExists = false };
            if (nameMatchedUser != null)
            {
                result.UsernameExists = true;
            }
            if (emailMatchUser != null)
            {
                result.EmailExists = true;
            }
            if (phoneMatchUser != null)
            {
                result.PhoneNumberExists = true;
            }
            return result;
        }
    }
}
