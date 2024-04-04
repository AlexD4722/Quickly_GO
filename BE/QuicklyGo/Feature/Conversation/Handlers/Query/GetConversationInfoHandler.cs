using AutoMapper;
using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Data.DTOs.Conversation;
using QuicklyGo.Feature.Conversation.Requests.Query;

namespace QuicklyGo.Feature.Conversation.Handlers.Query
{
    public class GetConversationInfoHandler : IRequestHandler<GetConversationInfoRequest, ConversationInfoDTO>
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IMapper _mapper;


        public GetConversationInfoHandler(IConversationRepository conversationRepository, IMapper mapper) {
            _conversationRepository = conversationRepository;
            _mapper = mapper;
        }

        public async Task<ConversationInfoDTO> Handle(GetConversationInfoRequest request, CancellationToken cancellationToken)
        {
            var conversation = await _conversationRepository.Get(request.Id); ;
            return _mapper.Map<ConversationInfoDTO>(conversation);
        }
    }
}
