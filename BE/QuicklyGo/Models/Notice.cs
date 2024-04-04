using QuicklyGo.Models.Common;

namespace QuicklyGo.Models
{
    public class Notice : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? UrlImg { get; set; }
        public string? Description { get; set; }
        public string UserId { get; set; }
        public NoticeStatus Status { get; set; }
        public User User { get; set; }
    }
}
