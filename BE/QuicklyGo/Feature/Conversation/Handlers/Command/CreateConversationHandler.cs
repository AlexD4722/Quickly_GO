using AutoMapper;
using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.Conversation.Requests.Command;

using QuicklyGo.Reponses;

namespace QuicklyGo.Feature.Conversation.Handlers.Command
{
    public class CreateConversationHandler : IRequestHandler<CreateConversationRequest, BaseCommandResponse>
    {
        private readonly IConversationRepository _ConversationRepo;
        private readonly IMapper _mapper;

        public CreateConversationHandler(IConversationRepository CRepo, IMapper mapper)
        {
            this._ConversationRepo = CRepo;
            this._mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateConversationRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var conversation = _mapper.Map<Models.Conversation>(request.ConversationInfo);
          
            if (_ConversationRepo == null)
            {
                response.Success = false;
                response.Message = "MessageRepository is null";
                return response;
            }
            if (conversation == null)
            {
                response.Success = false;
                response.Message = "Message is null";
                return response;
            }

            try
            {
                conversation = await _ConversationRepo.Add(conversation);
                await _ConversationRepo.Save();

                /*if (result){
                    response.Success = true;
                    response.Message = "Creation Successful";
                }else{
                    response.Success = false;
                    response.Message = "Create Failed";
                }*/
              

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Create Failed :" + ex;

            }
           
            return response;
        }
    }
}
