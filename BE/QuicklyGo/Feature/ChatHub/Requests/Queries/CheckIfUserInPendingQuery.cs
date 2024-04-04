using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Queries
{
    public class CheckIfUserInPendingQuery : IRequest<bool>
    {
        public string RequesterId { get; set; }
        public string ResponserId { get; set; }
        public CheckIfUserInPendingQuery(string requesterId, string responserId)
        {
            RequesterId = requesterId;
            ResponserId = responserId;
        }
    }
}
