using QuicklyGo.Data.Responsive;
using QuicklyGo.Models;

namespace QuicklyGo.Contracts.IData
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IMessageRepository MessageRepository { get; }
        IUserConversationRepository UserConversationRepository { get; }
        IConversationRepository ConversationRepository { get; }
        IReadMessageRepository ReadMessageRepository { get; }
        /*IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Message> MessageRepository { get; }
        IGenericRepository<UserConversation> UserConversationRepository { get; }
        IGenericRepository<Conversation> ConversationRepository { get; }
        IGenericRepository<ReadMessage> ReadMessageRepository { get; }*/
        Task Save();
    }
}
