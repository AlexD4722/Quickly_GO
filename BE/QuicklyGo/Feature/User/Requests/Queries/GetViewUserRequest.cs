using MediatR;
using QuicklyGo.Data.DTOs.User;

namespace QuicklyGo.Feature.User.Requests.Queries
{
    public class GetViewUserRequest : IRequest<GetToViewInfoUserDto>
    {
        public string KeyUser { get; set; }
    }
}
