using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Queries
{
    public class GetRelationshipsByUserIdsQuery : IRequest<List<QuicklyGo.Models.Relationship>>
    {
        public string UserId1 { get; set; }
        public string UserId2 { get; set; }
        public GetRelationshipsByUserIdsQuery(string userId1, string userId2)
        {
            UserId1 = userId1;
            UserId2 = userId2;
        }
    }
}
