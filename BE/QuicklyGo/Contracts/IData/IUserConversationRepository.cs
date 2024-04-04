using QuicklyGo.Data.DTOs.UserConvesation;
using QuicklyGo.Models;

namespace QuicklyGo.Contracts.IData
{
    public interface IUserConversationRepository : IGenericRepository<UserConversation>
    {
        public Task<ICollection<GetConversationByUserIdDto>> GetByUserId(string userId);
    }
}
