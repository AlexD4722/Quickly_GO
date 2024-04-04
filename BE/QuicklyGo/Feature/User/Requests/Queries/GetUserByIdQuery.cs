using MediatR;

namespace QuicklyGo.Feature.User.Requests.Queries
{
    public class GetUserByIdQuery : IRequest<QuicklyGo.Models.User>
    {
        public string UserId { get; set; }
        public GetUserByIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
