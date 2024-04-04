using QuicklyGo.Contracts.IData;
using QuicklyGo.Data.Responsive;
using QuicklyGo.Models;

namespace QuicklyGo.Data.Respositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(QuicklyGoDbContext dbContext) : base(dbContext)
        {
        }
        // add more methods here and need to implement in the interface
    }
}
