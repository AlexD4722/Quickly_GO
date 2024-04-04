using Microsoft.EntityFrameworkCore;
using QuicklyGo.Models.Common;

namespace QuicklyGo.Data
{
    public class AuditableDbContext : DbContext
    {
        public AuditableDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateAt = DateTime.Now;
                    entry.Entity.UpdateAt = DateTime.Now;
                }
            }
            var result = await base.SaveChangesAsync();

            return result;
        }
    }
}
