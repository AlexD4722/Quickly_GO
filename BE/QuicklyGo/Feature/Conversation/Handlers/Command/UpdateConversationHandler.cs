using AutoMapper;
using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Data.DTOs.Conversation;
using QuicklyGo.Feature.Conversation.Requests.Command;

namespace QuicklyGo.Feature.Conversation.Handlers.Command
{
    public class UpdateConversationHandler : IRequestHandler<UpdateConversationRequest, ConversationInfoDTO>
    {
        private IConversationRepository _conversationRepository { get; set; }
        private IMapper _mapper;

        public UpdateConversationHandler(IConversationRepository conversationRepository, IMapper mapper)
        {
            _conversationRepository = conversationRepository;
            _mapper = mapper;
        }

        public async Task<ConversationInfoDTO> Handle (UpdateConversationRequest request, CancellationToken cancellationToken)
        {
            var conversationExisted =  await _conversationRepository
                .FirstOrDefault(c => c.Id == request._conversationInfoDTO.Id);

            if (conversationExisted != null)
            {
                var conversation = _mapper.Map<Models.Conversation>(request._conversationInfoDTO);
                
                try
                {
                    await _conversationRepository.Update(conversation);
                    await _conversationRepository.Save();
                    return request._conversationInfoDTO;
                }catch (Exception ex)
                {
                    throw new ArgumentException("Error when update or save!", ex);
                }
            }
            else
            {
                throw new ArgumentException("Conversation not existed! check ID?");
            }
        }
    }
}
