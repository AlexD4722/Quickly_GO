using AutoMapper;
using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Data.DTOs.User;
using QuicklyGo.Feature.User.Requests.Queries;

namespace QuicklyGo.Feature.User.Handlers.Queries
{
    public class GetViewUserRequestHandler : IRequestHandler<GetViewUserRequest, GetToViewInfoUserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetViewUserRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task<GetToViewInfoUserDto> Handle(GetViewUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByKeyUnique(request.KeyUser);
            return _mapper.Map<GetToViewInfoUserDto>(user);
        }
    }
}
