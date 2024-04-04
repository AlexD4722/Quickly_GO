using MediatR;
using QuicklyGo.Data.DTOs.User;

namespace QuicklyGo.Feature.User.Requests.Queries
{
    public class GetInfoUserDetailRequest : IRequest<GetInfoUserDetailDto>
    {
        public string KeyUser { get; set; }
    }
}
