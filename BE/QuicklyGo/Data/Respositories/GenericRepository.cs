using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using System.Data;
using System.Linq.Expressions;

namespace QuicklyGo.Data.Responsive
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly QuicklyGoDbContext _dbContext;

        public GenericRepository(QuicklyGoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Add(T entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbContext.Add(entity);
            return entity;
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await Get(id);
            return entity != null;
        }

        public async Task<T> Get<IS>(IS id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public async Task<List<T>> ToList(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public DbSet<T> Query()
        {
            return _dbContext.Set<T>();
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
