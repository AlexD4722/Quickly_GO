using QuicklyGo.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace QuicklyGo.Models
{
    public class UserConversation : BaseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ConversationId { get; set; }
        public DateTime? LastSeenMessage { get; set; }
        public UserConversationStatus Status { get; set; }
        public Conversation Conversation { get; set; }
        public User User { get; set; }
    }
}
