using AutoMapper;
using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Data.DTOs.User;
using QuicklyGo.Feature.User.Requests.Queries;

namespace QuicklyGo.Feature.User.Handlers.Queries
{
    public class GetListInfoDetailUsersRequestHandler : IRequestHandler<GetListInfoDetailUsersRequest, List<GetInfoUserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetListInfoDetailUsersRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task<List<GetInfoUserDto>> Handle(GetListInfoDetailUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<List<GetInfoUserDto>>(users);
        }
    }
}
