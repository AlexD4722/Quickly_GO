namespace QuicklyGo.Data.DTOs.Message
{
    public class CreateMessageDto : IMessageDto
    {
        public string CreatorId { get; set; }
        public string ConversationId { get; set; }
        public string BodyContent { get; set; }
    }
}
