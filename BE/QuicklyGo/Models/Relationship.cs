using QuicklyGo.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace QuicklyGo.Models
{
    public class Relationship: BaseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public string? Alias { get; set; }
        public RelationshipStatus Status { get; set; }
        public User User { get; set; }
        public User Friend { get; set; }    
    }
}
