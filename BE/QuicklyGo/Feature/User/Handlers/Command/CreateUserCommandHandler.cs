using AutoMapper;
using MediatR;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Feature.User.Requests.Command;
using QuicklyGo.Reponses;
using QuicklyGo.Models;
using QuicklyGo.Data.DTOs.User.Validator;
using Azure;
using QuicklyGo.Unit;
using QuicklyGo.Utils;

namespace QuicklyGo.Feature.User.Handlers.Command
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // validate user data
            var validator = new CreateUserDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateUserDto);
            var response = new BaseCommandResponse();
            if (validationResult.IsValid == false)
            {
                // return bad request with errors
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

            var user = _mapper.Map<QuicklyGo.Models.User>(request.CreateUserDto);
            user.Status = UserStatus.Pending;

            // generate verify code
            var codeVerify = CreateGenerateUniqueKey.GenerateUniqueKey(6);
            user.VerifyCode = codeVerify;
            user = await _unitOfWork.UserRepository.Add(user);
            try
            {
                await MailSender.SendMailAsync(user.Email, "this is code velify email", codeVerify);
            }
            catch (Exception)
            {

                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = new List<string> { "Send mail failed" };
                return response;
            }

            await _unitOfWork.Save();
            response.Success = true;
            response.Data = user.Id;
            response.Message = "Creation Successful";
            return response;
        }
    }
}
