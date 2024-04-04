using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Queries
{
    public class GetListFriendRequests : IRequest<IEnumerable<QuicklyGo.Models.Relationship>>
    {
        public string UserId { get; set; }
        public GetListFriendRequests(string userId)
        {
            UserId = userId;
        }
    }
}
