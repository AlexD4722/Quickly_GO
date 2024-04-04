
using QuicklyGo.Models;
namespace QuicklyGo.Data.DTOs.UserConvesation
{
    public interface IUserConversation 
    {
        public string Id { get; set; }
        public string ConversationId { get; set; }
        public string? LastSeenMessage { get; set; }
        public int IsActive { get; set; }
        public Models.Conversation Conversation { get; set; }
    }
}
