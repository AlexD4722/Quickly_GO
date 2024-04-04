using AutoMapper;
using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Data.DTOs.User;
using QuicklyGo.Feature.User.Requests.Queries;

namespace QuicklyGo.Feature.User.Handlers.Queries
{
    public class GetUserListHandler : IRequestHandler<GetUserListRequest, List<GetInfoUserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserListHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<GetInfoUserDto>> Handle(GetUserListRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<List<GetInfoUserDto>>(users);
        }
    }
}
