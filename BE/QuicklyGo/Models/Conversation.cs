using QuicklyGo.Models.Common;

namespace QuicklyGo.Models
{
    public class Conversation : BaseEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? UrlImg { get; set; }
        public string? Description { get; set; }
        public bool Group { get; set; }
        public ConversationStatus Status { get; set; }
        public ICollection<UserConversation> UserConversations { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
