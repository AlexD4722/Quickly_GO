namespace QuicklyGo.Data.DTOs.User
{
    public class GetInfoUserDto : IUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string VerifyCode { get; set; }
        public string KeyUniqueNow { get; set; }
        public string Status { get; set; }
    }
}
