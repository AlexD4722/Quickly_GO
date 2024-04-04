using QuicklyGo.Contracts.IData;
using QuicklyGo.Data.Responsive;
using QuicklyGo.Models;

namespace QuicklyGo.Data.Respositories
{
    public class ReadMessageRepository : GenericRepository<ReadMessage>, IReadMessageRepository
    {
        public ReadMessageRepository(QuicklyGoDbContext context) : base(context)
        {
        }
    }
}
