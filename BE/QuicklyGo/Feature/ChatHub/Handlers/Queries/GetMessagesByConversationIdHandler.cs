using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Queries;

namespace QuicklyGo.Feature.ChatHub.Handlers.Queries
{
    public class GetMessagesByConversationIdHandler : IRequestHandler<GetMessagesByConversationIdQuery, List<QuicklyGo.Models.Message>>
    {
        public IGenericRepository<QuicklyGo.Models.Message> _messageRepository;
        public GetMessagesByConversationIdHandler(IGenericRepository<QuicklyGo.Models.Message> messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<List<QuicklyGo.Models.Message>> Handle(GetMessagesByConversationIdQuery request, CancellationToken cancellationToken)
        {
            if (request.ConversationId == null)
            {
                throw new ArgumentNullException("ConversationId is null");
            }
            if (request.NumberOfMessages <= 0)
            {
                throw new ArgumentOutOfRangeException("Number Of Messages must be greater than 0");
            }
            return await _messageRepository
                .Query()
                .Where(m => m.ConversationId == request.ConversationId)
                .Select(m => new QuicklyGo.Models.Message
                {
                    Id = m.Id,
                    BodyContent = m.BodyContent,
                    CreateAt = m.CreateAt,
                    CreatorId = m.CreatorId,
                    ConversationId = m.ConversationId,
                    Creator = new QuicklyGo.Models.User
                    {
                        Id = m.Creator.Id,
                        UserName = m.Creator.UserName,
                        UrlImgAvatar = m.Creator.UrlImgAvatar,
                        FirstName = m.Creator.FirstName,
                        LastName = m.Creator.LastName,
                    }
                })
                .OrderByDescending(m => m.CreateAt)
                .Take(request.NumberOfMessages)
                .ToListAsync();
        }
    }
}
