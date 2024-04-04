using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.User.Requests.Command;

namespace QuicklyGo.Feature.User.Handlers.Command
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, MediatR.Unit>
    {
        public IGenericRepository<QuicklyGo.Models.User> _repository;
        public UpdateUserHandler(IGenericRepository<QuicklyGo.Models.User> repository)
        {
            _repository = repository;
        }
        public async Task<MediatR.Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.User;
            await _repository.Update(user);
            await _repository.Save();
            return MediatR.Unit.Value;
        }
    }
}
