using MediatR;
using QuicklyGo.Data.DTOs.User;
using QuicklyGo.Reponses;

namespace QuicklyGo.Feature.User.Requests.Command
{
    public class CreateUserCommand : IRequest<BaseCommandResponse>
    {
        public CreateUserDto CreateUserDto { get; set; }
        public CreateUserCommand(CreateUserDto createUserDto)
        {
            CreateUserDto = createUserDto;
        }
    }
}
