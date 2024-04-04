using QuicklyGo.Models;

namespace QuicklyGo.Data.DTOs.User
{
    public class UpdateUserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
