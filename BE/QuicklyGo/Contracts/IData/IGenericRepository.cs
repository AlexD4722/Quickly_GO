using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace QuicklyGo.Contracts.IData
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get<IS>(IS id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Add(T entity);
        Task<bool> Exists(int id);
        Task Update(T entity);
        Task Delete(T entity);
        Task Save();
        Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<List<T>> ToList(Expression<Func<T, bool>> predicate);
        DbSet<T> Query();
    }
}
