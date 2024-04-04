using FluentValidation;
using QuicklyGo.Data.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuicklyGo.Data.DTOs.User.Validator
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            Include(new IUserDtoValidator());
            RuleFor(x => x.FirstName)
                               .NotEmpty().WithMessage("{PropertyName} is required")
                               .NotNull()
                               .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");
            RuleFor(x => x.LastName)
                               .NotEmpty().WithMessage("{PropertyName} is required")
                               .NotNull()
                               .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");
            RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("{PropertyName} is required")
                    .NotNull()
                    .MinimumLength(8).WithMessage("{PropertyName} must be at least 8 characters long")
                    .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");
        }
    }
}
