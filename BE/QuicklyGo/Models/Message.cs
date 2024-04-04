using QuicklyGo.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace QuicklyGo.Models
{
    public class Message : BaseEntity
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string ConversationId { get; set; }
        public string BodyContent { get; set; }
        public MessageStatus Status { get; set; }
        public User Creator { get; set; }
        public Conversation Conversation { get; set; }
        public ICollection<ReadMessage> ReadMessages { get; set; }
        public ICollection<UserConversation> UserConversations { get; set; }
    }
}
