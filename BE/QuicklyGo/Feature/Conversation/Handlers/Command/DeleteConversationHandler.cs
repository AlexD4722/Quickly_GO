using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.Conversation.Requests.Command;

namespace QuicklyGo.Feature.Conversation.Handlers.Command
{
    public class DeleteConversationHandler : IRequestHandler<DeleteConversationRequest, bool>
    {
        protected IConversationRepository _conversationRepository;
        public DeleteConversationHandler(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        public async Task<bool> Handle(DeleteConversationRequest request, CancellationToken cancellationToken)
        {
            var conversationExisted = await _conversationRepository.FirstOrDefault(c => c.Id == request.Id);
            var conversation = await _conversationRepository.Get(request.Id);
            if (conversationExisted != null)
            {
                await _conversationRepository.Delete(conversation);

                try
                {
                    await _conversationRepository.Save();
                    return true;

                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Error when saving!", ex);
                }
            }
            else
            {
                throw new ArgumentException("Conversation not existed! cant  find by ID.");
            }
        }
    }
}
