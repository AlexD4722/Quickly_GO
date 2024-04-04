using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Queries
{
    public class CheckIfUserActiveQuery : IRequest<bool>
    {
        public string UserId { get; set; }
        public CheckIfUserActiveQuery(string userId)
        {
            UserId = userId;
        }
    }
}
