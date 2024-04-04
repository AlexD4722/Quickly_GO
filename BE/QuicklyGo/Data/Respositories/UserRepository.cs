using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Models;

namespace QuicklyGo.Data.Responsive
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(QuicklyGoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetByKeyUnique(string key)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == key);
        }

        public async Task<bool> UpdateStatusUser(string id, UserStatus statusNew)
        {
            var entity = await _dbContext.Users.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            try
            {
                _dbContext.Entry(entity).Property("Status").CurrentValue = statusNew;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch{ return false; }

            
        }
    }
}
