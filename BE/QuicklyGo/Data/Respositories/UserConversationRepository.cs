using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Data.DTOs.UserConvesation;
using QuicklyGo.Data.Responsive;
using QuicklyGo.Models;

namespace QuicklyGo.Data.Respositories
{
    public class UserConversationRepository : GenericRepository<UserConversation>, IUserConversationRepository
    {
        public UserConversationRepository(QuicklyGoDbContext context) : base(context)
        {

        }

        public async Task<ICollection<GetConversationByUserIdDto>> GetByUserId(string userId)
        {

            var userConversations = await _dbContext.UserConversations
                .Where(uc => uc.UserId == userId)
                .Include(uc => uc.Conversation)
                .Select(uc => new GetConversationByUserIdDto
                {
                    ConversationId = uc.ConversationId,
                    Conversation = new Conversation
                    {
                        Id = uc.Conversation.Id,
                        Name = uc.Conversation.Name,
                        UrlImg = uc.Conversation.UrlImg,
                        Description = uc.Conversation.Description,
                        Group = uc.Conversation.Group,
                        Status = uc.Conversation.Status,
                        CreateAt = uc.Conversation.CreateAt,
                        UpdateAt = uc.Conversation.UpdateAt,
                        Messages = uc.Conversation.Messages
                    }
                })
                .ToListAsync();
            return userConversations;
        }
    }
}
