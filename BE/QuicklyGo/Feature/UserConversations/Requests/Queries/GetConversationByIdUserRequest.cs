using MediatR;
using QuicklyGo.Data.DTOs.UserConvesation;
using QuicklyGo.Models;

namespace QuicklyGo.Feature.UserConversations.Requests.Queries
{
    public class GetConversationByIdUserRequest : IRequest<ICollection<GetConversationByUserIdDto>>
    {
        public string UserId { get; set; }
        public GetConversationByIdUserRequest(string userId)
        {
            UserId = userId;
        }
    }
}
