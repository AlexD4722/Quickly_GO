using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Commands;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.ChatHub.Handlers.Commands
{
    public class AddReadMessagesHandler : IRequestHandler<AddReadMessagesCommand, MediatR.Unit>
    {
        public IGenericRepository<ReadMessage> _readMessageRepository;
        public AddReadMessagesHandler(IGenericRepository<ReadMessage> repository)
        {
            _readMessageRepository = repository;
        }
        public async Task<MediatR.Unit> Handle(AddReadMessagesCommand request, CancellationToken cancellationToken)
        {
            foreach (var messageId in request.ReadMessages)
            {
                if (_readMessageRepository.Query().Any(x => x.MessageId == messageId && x.ReceipientId == request.UserId))
                {
                    continue;
                }
                var readMessage = new ReadMessage
                {
                    MessageId = messageId,
                    ReceipientId = request.UserId
                };
                await _readMessageRepository.Add(readMessage);
            }
            await _readMessageRepository.Save();
            return MediatR.Unit.Value;
        }
    }
}