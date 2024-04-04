using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Data.Responsive;
using QuicklyGo.Models;

namespace QuicklyGo.Data.Respositories
{
    public class RelationshipRepository : GenericRepository<Relationship>, IRelationshipRepository
    {
        public RelationshipRepository(QuicklyGoDbContext context) : base(context)
        {
        }

        public async Task<Relationship> DeleteRelationshipByUserIdAndFriendId(string userId, string friendId)
        {
            var relationship = await _dbContext.Relationships
                                   .FirstOrDefaultAsync(r => r.UserId == userId && r.FriendId == friendId);

            if (relationship != null)
            {
                _dbContext.Relationships.Remove(relationship);
                await _dbContext.SaveChangesAsync();
            }

            return relationship;
        }

        public async Task<Relationship> GetRelationshipByUserIdAndFriendId(string userId, string friendId)
        {
            return await _dbContext.Relationships
                           .FirstOrDefaultAsync(r => r.UserId == userId && r.FriendId == friendId);
        } 
    }
}
