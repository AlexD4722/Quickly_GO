using AutoMapper;
using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Data.DTOs.User;
using QuicklyGo.Feature.User.Requests.Queries;

namespace QuicklyGo.Feature.User.Handlers.Queries
{
    public class GetUserDetailRequestHandler : IRequestHandler<GetInfoUserDetailRequest, GetInfoUserDetailDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserDetailRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task<GetInfoUserDetailDto> Handle(GetInfoUserDetailRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByKeyUnique(request.KeyUser);
            return _mapper.Map<GetInfoUserDetailDto>(user);
        }
    }
}
