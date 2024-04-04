using MediatR;
using QuicklyGo.Data.DTOs.User;

namespace QuicklyGo.Feature.User.Requests.Queries
{
    public class GetUserListRequest : IRequest<List<GetInfoUserDto>>
    {
    }
}
