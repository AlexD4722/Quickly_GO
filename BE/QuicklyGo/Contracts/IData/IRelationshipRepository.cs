using QuicklyGo.Models;

namespace QuicklyGo.Contracts.IData
{
    public interface IRelationshipRepository : IGenericRepository<Relationship>
    {
        public Task<Relationship> GetRelationshipByUserIdAndFriendId(string userId, string friendId);
        public Task<Relationship> DeleteRelationshipByUserIdAndFriendId(string userId, string friendId);
    }
}
