using QuicklyGo.Models;

namespace QuicklyGo.Contracts.IData
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByKeyUnique(string key);
        Task<bool> UpdateStatusUser(string id, UserStatus statusNew);
    }
}
