using MediatR;
using System.Linq.Expressions;

namespace QuicklyGo.Feature.User.Requests.Queries
{
    public class GetUsersMatchRequest : IRequest<List<QuicklyGo.Models.User>>
    {
        public Expression<Func<QuicklyGo.Models.User, bool>> Predicate { get; set; }
        public GetUsersMatchRequest(Expression<Func<QuicklyGo.Models.User, bool>> predicate)
        {
            Predicate = predicate;
        }
    }
}
