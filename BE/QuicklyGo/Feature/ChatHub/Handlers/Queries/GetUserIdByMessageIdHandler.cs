using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Queries;

namespace QuicklyGo.Feature.ChatHub.Handlers.Queries
{
    public class GetUserIdByMessageIdHandler : IRequestHandler<GetUserIdByMessageIdQuery, string>
    {
        public IGenericRepository<QuicklyGo.Models.Message> _messageRepository;
        public GetUserIdByMessageIdHandler(IGenericRepository<QuicklyGo.Models.Message> messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<string> Handle(GetUserIdByMessageIdQuery request, CancellationToken cancellationToken)
        {
            var message = await _messageRepository.Get(request.MessageId);
            return message.CreatorId;
        }
    }
}
