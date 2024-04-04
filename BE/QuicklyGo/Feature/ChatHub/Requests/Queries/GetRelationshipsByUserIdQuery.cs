using MediatR;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.ChatHub.Requests.Queries
{
    public class GetRelationshipsByUserIdQuery :IRequest<IEnumerable<QuicklyGo.Models.Relationship>>
    {
        public string UserId { get; set; }
        public GetRelationshipsByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
