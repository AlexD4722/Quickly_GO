using QuicklyGo.Models;

namespace QuicklyGo.Data.DTOs.Conversation
{
    public class CreateConversationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Group { get; set; }
        public ICollection<string> UserIds { get; set; }
    }
}
