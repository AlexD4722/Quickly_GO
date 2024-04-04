using AutoMapper;
using MediatR;
using NuGet.Protocol.Plugins;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.Message.Requests.Command;
using QuicklyGo.Reponses;

namespace QuicklyGo.Feature.Message.Handlers.Command
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateMessageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var message = _mapper.Map<QuicklyGo.Models.Message>(request.MessageCreate);
            if (_unitOfWork == null)
            {
                response.Success = false;
                response.Message = "UnitOfWork is null";
                return response;
            }
            if (_unitOfWork.MessageRepository == null)
            {
                response.Success = false;
                response.Message = "MessageRepository is null";
                return response;
            }
            if (message == null)
            {
                response.Success = false;
                response.Message = "Message is null";
                return response;
            }
            message = await _unitOfWork.MessageRepository.Add(message);
            await _unitOfWork.Save();
            response.Success = true;
            response.Message = "Creation Successful";
            return response;
        }
    }
}
