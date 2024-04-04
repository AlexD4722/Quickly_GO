using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.User.Requests.Command;

namespace QuicklyGo.Feature.User.Handlers.Command
{
    public class UpdateStatusUserCommandHandler : IRequestHandler<UpdateStatusUserRequest, Boolean>
    {
        private readonly IUserRepository _userRepository;
        public UpdateStatusUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(UpdateStatusUserRequest request, CancellationToken cancellationToken)
        {
            
            var result = await _userRepository.UpdateStatusUser(request.UserId, request.Status);
            if (result)
            {
                return true;
            }
            return false;
        }
    }
}
