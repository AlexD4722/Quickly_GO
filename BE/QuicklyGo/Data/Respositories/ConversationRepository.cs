using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Data.Responsive;
using QuicklyGo.Models;

namespace QuicklyGo.Data.Respositories
{
    public class ConversationRepository : GenericRepository<Conversation>, IConversationRepository
    {
        public ConversationRepository(QuicklyGoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
