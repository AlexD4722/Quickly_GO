using QuicklyGo.Data.DTOs.Common;

namespace QuicklyGo.Data.DTOs.User
{
    public class GetToViewInfoUserDto 
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UrlImgAvatar { get; set; }
        public string UrlBackground { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
    }
}
