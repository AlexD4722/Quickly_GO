using MediatR;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.User.Requests.Command
{
    public class UpdateStatusUserRequest : IRequest<Boolean>
    {
        public string UserId { get; set; }
        public UserStatus Status { get; set; }
        public UpdateStatusUserRequest(string userId, UserStatus status)
        {
            UserId = userId;
            Status = status;
        }
    }
}
