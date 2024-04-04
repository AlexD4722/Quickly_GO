using MediatR;
using QuicklyGo.Data.DTOs.User;

namespace QuicklyGo.Feature.User.Requests.Queries
{
    public class ConfirmUserLoginRequest : IRequest<QuicklyGo.Models.User?>
    {
        public LoginUserDto LoginUserDto { get; set; }
        public ConfirmUserLoginRequest(LoginUserDto loginUserDto)
        {
            LoginUserDto = loginUserDto;
        }
    }
}
