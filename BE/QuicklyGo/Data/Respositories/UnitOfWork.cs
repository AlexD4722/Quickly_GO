using QuicklyGo.Contracts.IData;
using QuicklyGo.Data.Responsive;
using QuicklyGo.Models;

namespace QuicklyGo.Data.Respositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuicklyGoDbContext _context;
        /*        private readonly IHttpContextAccessor _httpContextAccessor;*/
        private IUserRepository _userRepository;
        private IMessageRepository _messageRepository;
        private IConversationRepository _conversationRepository;
        private IUserConversationRepository _userConversationRepository;
        private IReadMessageRepository _readMessageRepository;
        /*private IGenericRepository<User> _userRepository { get; }
        private IGenericRepository<Message> _messageRepository { get; }
        private IGenericRepository<UserConversation> _userConversationRepository { get; }
        private IGenericRepository<Conversation> _conversationRepository { get; }
        private IGenericRepository<ReadMessage> _readMessageRepository { get; }*/

        public UnitOfWork(QuicklyGoDbContext context,
            IUserRepository userReponsitory,
            IMessageRepository messageRepository,
            IConversationRepository conversationRepository,
            IUserConversationRepository userConversationRepository,
            IReadMessageRepository readMessageRepository)
        {
            _context = context;
            _userRepository = userReponsitory;
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
            _userConversationRepository = userConversationRepository;
            _readMessageRepository = readMessageRepository;
        }

        public IUserRepository UserRepository => _userRepository;

        public IMessageRepository MessageRepository => _messageRepository;

        public IUserConversationRepository UserConversationRepository => _userConversationRepository;

        public IConversationRepository ConversationRepository => _conversationRepository;

        public IReadMessageRepository ReadMessageRepository => _readMessageRepository;

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
