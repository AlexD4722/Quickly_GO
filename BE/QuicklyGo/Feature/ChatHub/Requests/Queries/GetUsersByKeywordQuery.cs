using MediatR;

namespace QuicklyGo.Feature.ChatHub.Requests.Queries
{
    public class GetUsersByKeywordQuery : IRequest<List<Models.User>>
    {
        public string Keyword { get; set; }
        public string CallerId { get; set; }
        public GetUsersByKeywordQuery(string keyword, string callerId)
        {
            Keyword = keyword;
            CallerId = callerId;
        }
    }
}
