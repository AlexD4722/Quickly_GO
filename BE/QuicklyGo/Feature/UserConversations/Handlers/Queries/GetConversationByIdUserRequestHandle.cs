using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Data.DTOs.UserConvesation;
using QuicklyGo.Feature.UserConversations.Requests.Queries;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.UserConversations.Handlers.Queries
{
    public class GetConversationByIdUserRequesHandle : IRequestHandler<GetConversationByIdUserRequest, ICollection<GetConversationByUserIdDto>>
    {
        public IUserConversationRepository _userConversationRepository;
        public IUserRepository _userRepository;
        public IConversationRepository _conversationRepository;
        public GetConversationByIdUserRequesHandle(IUserConversationRepository userConversationRepository, IConversationRepository conversationRepository, IUserRepository userRepository)
        {
            _userConversationRepository = userConversationRepository;
            _conversationRepository = conversationRepository;
            _userRepository = userRepository;
        }
        public async Task<ICollection<GetConversationByUserIdDto>> Handle(GetConversationByIdUserRequest request, CancellationToken cancellationToken)
        {
            /*var UserConversations = await _userConversationRepository.GetByUserId(request.UserId);*/
            var UserConversations = await _userConversationRepository.GetByUserId(request.UserId);
            return UserConversations;
        }
    }
}
