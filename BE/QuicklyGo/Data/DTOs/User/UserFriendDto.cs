using QuicklyGo.Models;

namespace QuicklyGo.Data.DTOs.User
{
    public class UserFriendDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? UrlImgAvatar { get; set; }
        public UserStatus Status { get; set; }
    }
}
