using QuicklyGo.Models;

namespace QuicklyGo.Data.DTOs.UserConvesation
{
    public class ChatConversationDetialDto
    {
        public string ConversationId { get; set; }
        public string Name { get; set; }
        public string UrlImg { get; set; }
        public string? LastMessage { get; set; }
        public DateTime? LastReatTime { get; set; }
        public string Description { get; set; }
        public bool Group { get; set; }
        public ConversationStatus Status { get; set; }
    }
}
