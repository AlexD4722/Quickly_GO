using MediatR;
using QuicklyGo.Data.DTOs.Message;
using QuicklyGo.Data.DTOs.User;
using QuicklyGo.Reponses;

namespace QuicklyGo.Feature.Message.Requests.Command
{
    public class CreateMessageCommand : IRequest<BaseCommandResponse>
    {
        public CreateMessageDto MessageCreate { get; set; }
        public CreateMessageCommand(CreateMessageDto createMessageDto)
        {
            MessageCreate = createMessageDto;
        }
    }
}
