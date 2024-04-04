using MediatR;
using NuGet.Protocol.Plugins;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Commands;

namespace QuicklyGo.Feature.ChatHub.Handlers.Commands
{
    public class UpdateMessageHandler : IRequestHandler<UpdateMessageCommand>
    {
        public IGenericRepository<QuicklyGo.Models.Message> _messageRepository;
        public UpdateMessageHandler(IGenericRepository<QuicklyGo.Models.Message> messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<MediatR.Unit> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            await _messageRepository.Update(request.Message);
            await _messageRepository.Save();
            return MediatR.Unit.Value;
        }
    }
}
