using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Queries
{
    public class CheckIfUserInDeclinedQuery : IRequest<bool>
    {
        public string RequesterId { get; set; }
        public string ResponserId { get; set; }
        public CheckIfUserInDeclinedQuery(string requesterId, string responserId)
        {
            RequesterId = requesterId;
            ResponserId = responserId;
        }
    }
}
