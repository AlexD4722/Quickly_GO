using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Commands;

namespace QuicklyGo.Feature.ChatHub.Handlers.Commands
{
    public class AddNewMessagesHandler : IRequestHandler<AddNewMessagesCommand, MediatR.Unit>
    {
        public IGenericRepository<QuicklyGo.Models.Message> _messageRepository;
        public AddNewMessagesHandler(IGenericRepository<QuicklyGo.Models.Message> messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<MediatR.Unit> Handle(AddNewMessagesCommand request, CancellationToken cancellationToken)
        {
            foreach (var message in request.Messages)
            {
                await _messageRepository.Add(message);
            }
            await _messageRepository.Save();
            return MediatR.Unit.Value;
        }
    }
}
