using MediatR;
using QuicklyGo.Data.DTOs.User;
using QuicklyGo.Reponses;

namespace QuicklyGo.Feature.User.Requests.Queries
{
    public class CheckCreatingUserInfoRequest : IRequest<CheckCreatingUserInfoResponse>
    {
        public CreateUserDto User { get; set; }
        public CheckCreatingUserInfoRequest(CreateUserDto user)
        {
            User = user;
        }
    }
}
