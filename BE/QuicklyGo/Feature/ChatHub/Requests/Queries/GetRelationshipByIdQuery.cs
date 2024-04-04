using MediatR;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.ChatHub.Requests.Queries
{
    public class GetRelationshipByIdQuery : IRequest<Relationship>
    {
        public int RelationshipId { get; }
        public GetRelationshipByIdQuery(int id)
        {
            RelationshipId = id;
        }
    }
}
