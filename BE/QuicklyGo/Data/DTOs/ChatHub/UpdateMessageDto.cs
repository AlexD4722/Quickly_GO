using QuicklyGo.Models;

namespace QuicklyGo.Data.DTOs.ChatHub
{
    public class UpdateMessageDto
    {
        public int Id { get; set; }
        public string BodyContent { get; set; }
        public string ConversationId { get; set; }
        public MessageStatus Status { get; set; }
    }
}
