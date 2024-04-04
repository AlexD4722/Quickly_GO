using MediatR;
using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Data.DTOs.Message;
using QuicklyGo.Feature.ChatHub.Requests.Queries;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.ChatHub.Handlers.Queries
{
    public class GetConversationsByUserIdQueryHandler : IRequestHandler<GetConversationsByUserIdQuery, List<QuicklyGo.Models.Conversation>>
    {
        public IGenericRepository<QuicklyGo.Models.Conversation> _conversationRepository;
        public GetConversationsByUserIdQueryHandler(IGenericRepository<QuicklyGo.Models.Conversation> repository)
        {
            _conversationRepository = repository;
        }
        public async Task<List<QuicklyGo.Models.Conversation>> Handle(GetConversationsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var conversations = await _conversationRepository.Query()
                .Include(c => c.UserConversations)
                .ThenInclude(uc => uc.User)
                .Include(c => c.Messages)
                .Select(c => new QuicklyGo.Models.Conversation
                {
                    Id = c.Id,
                    Name = c.Name,
                    UrlImg = c.UrlImg,
                    Description = c.Description,
                    Group = c.Group,
                    CreateAt = c.CreateAt,
                    UpdateAt = c.UpdateAt,
                    UserConversations = c.UserConversations,
                    Messages = c.Messages,
                })
                .Where(c => c.UserConversations.Any(uc => uc.UserId == request.UserId))
                .AsSplitQuery()
                .ToListAsync();

            return conversations;
        }
    }
}
