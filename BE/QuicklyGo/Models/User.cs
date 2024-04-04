using QuicklyGo.Models.Common;

namespace QuicklyGo.Models
{
    public class User : BaseEntity
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public string Password { get; set; }
        public string VerifyCode { get; set; }
        public string? UrlImgAvatar { get; set; }
        public string? UrlBackground { get; set; }
        public string ? Description { get; set; }
        public string ? Location { get; set; }
        public UserStatus Status { get; set; }
        public ICollection<ReadMessage> ReadMessages { get; set; }
        public ICollection<UserConversation> UserConversations { get; set; }
        public ICollection<Relationship> Relationships { get; set; }
        public ICollection<Relationship> FriendRelationships { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Notice> Notices { get; set; }
    }
}
