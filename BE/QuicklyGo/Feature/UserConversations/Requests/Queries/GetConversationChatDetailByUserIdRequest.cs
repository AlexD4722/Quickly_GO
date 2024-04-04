using MediatR;
using QuicklyGo.Data.DTOs.UserConvesation;
using QuicklyGo.Feature.UserConversations.Handlers.Queries;

namespace QuicklyGo.Feature.UserConversations.Requests.Queries
{
    public class GetConversationChatDetailByUserIdRequest: IRequest<ResponseChatConversation>
    {
        public string UserId { get; set; }  
        public GetConversationChatDetailByUserIdRequest(string userId)
        {
            UserId = userId;
        }
    }
}
