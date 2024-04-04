namespace QuicklyGo.Hubs
{
    public interface ChatClientSide
    {
        Task RefreshConversation(object conversation);
        Task SendMessageFail(object message);
        Task ReceiveReadNotify(object notify);
        Task Connected(object conversations);
        Task ReceiveFriendRequest(object request);
        Task ReceiveFriendRequestResponse(object response);
        Task RefreshRelationShip(object response);
        Task RefreshChats(object chats);
        Task RefreshLookForUser(object User);
    }
}
