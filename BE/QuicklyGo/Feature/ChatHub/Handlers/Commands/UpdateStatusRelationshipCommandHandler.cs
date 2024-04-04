using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.ChatHub.Requests.Commands;
using QuicklyGo.Models;
using QuicklyGo.Reponses;

namespace QuicklyGo.Feature.ChatHub.Handlers.Commands
{
    public class UpdateStatusRelationshipCommandHandler : IRequestHandler<UpdateStatusRelationshipCommand, BaseCommandResponse>
    {
        public IRelationshipRepository _relationshipRepository;
        public UpdateStatusRelationshipCommandHandler(IRelationshipRepository relationshipRepository)
        {
            _relationshipRepository = relationshipRepository;
        }
        public async Task<BaseCommandResponse> Handle(UpdateStatusRelationshipCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            try
            {
                // Get the existing relationship from the database
                var existingRelationship = await _relationshipRepository.GetRelationshipByUserIdAndFriendId(request.UserId, request.FriendId);

                if (existingRelationship != null)
                {
                    // Update the status of the relationship
                    existingRelationship.Status = RelationshipStatus.Active;

                    // Save the changes
                    _relationshipRepository.Update(existingRelationship);
                    await _relationshipRepository.Save();

                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Relationship not found";
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
