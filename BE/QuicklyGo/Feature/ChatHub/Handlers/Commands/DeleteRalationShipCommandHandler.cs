using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Commands;
using QuicklyGo.Reponses;

namespace QuicklyGo.Feature.ChatHub.Handlers.Commands
{
    public class DeleteRalationShipCommandHandler : IRequestHandler<DeleteRalationShipCommand, BaseCommandResponse>
    {
        public IRelationshipRepository _relationshipRepository;
        public DeleteRalationShipCommandHandler(IRelationshipRepository relationshipRepository)
        {
            _relationshipRepository = relationshipRepository;
        }
        public async Task<BaseCommandResponse> Handle(DeleteRalationShipCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {
                var relationship = await _relationshipRepository.DeleteRelationshipByUserIdAndFriendId(request.UserId, request.FriendId);

                if (relationship != null)
                {
                    response.Success = true;
                    response.Message = "Relationship deleted successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Relationship not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
