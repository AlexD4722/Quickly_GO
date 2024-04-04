using MediatR;

namespace QuicklyGo.Feature.User.Requests.Command
{
    public class UpdateUserCommand : IRequest<MediatR.Unit>
    {
        public QuicklyGo.Models.User User { get; set; }
        public UpdateUserCommand(QuicklyGo.Models.User user)
        {
            User = user;
        }
    }
}
