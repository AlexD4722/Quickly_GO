using QuicklyGo.Data.DTOs.Common;

namespace QuicklyGo.Data.DTOs.Message
{
    public class MessageDto : BaseDto, IMessageDto
    {
        public string CreatorId { get; set; }
        public string ConversationId { get; set; }
        public string BodyContent { get; set; }
    }
}
