using FluentValidation;
using QuicklyGo.Data.DTOs.User;
using QuicklyGo.Data.DTOs.User.Validator;

namespace QuicklyGo.Data.DTOs.Message.Validator
{
    public class CreateMessageValidator : AbstractValidator<CreateMessageDto>
    {
        public CreateMessageValidator()
        {
            Include(new IMessageValidator());
        }
    }
}
