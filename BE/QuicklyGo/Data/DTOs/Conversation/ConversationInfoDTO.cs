using QuicklyGo.Data.DTOs.Common;
using QuicklyGo.Models;

namespace QuicklyGo.Data.DTOs.Conversation
{
    public class ConversationInfoDTO 

    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string UrlImg { get; set; }
        public string Description { get; set; }
        public bool Group { get; set; }
        public bool IsActive { get; set; }
    }
}
