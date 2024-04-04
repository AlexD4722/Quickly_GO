using FluentValidation;
using QuicklyGo.Data.DTOs.User;

namespace QuicklyGo.Data.DTOs.User.Validator
{
    public class IUserDtoValidator : AbstractValidator<IUserDto>
    {
        public IUserDtoValidator()
        {
            RuleFor(x => x.UserName)
                    .NotEmpty().WithMessage("{PropertyName} is required")
                    .NotNull()
                    .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");
            RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("{PropertyName} is required")
                    .NotNull()
                    .EmailAddress().WithMessage("{PropertyName} must be a valid email address")
                    .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");
            RuleFor(x => x.PhoneNumber)
                    .NotEmpty().WithMessage("{PropertyName} is required")
                    .NotNull();
        }
    }
}
