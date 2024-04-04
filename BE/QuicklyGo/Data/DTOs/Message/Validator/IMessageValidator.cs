using FluentValidation;
using QuicklyGo.Data.DTOs.User;

namespace QuicklyGo.Data.DTOs.Message.Validator
{
    public class IMessageValidator : AbstractValidator<IMessageDto>
    {
        public IMessageValidator()
        {
            RuleFor(x => x.CreatorId)
                    .NotEmpty().WithMessage("{PropertyName} is required")
                    .NotNull();
            RuleFor(x => x.ConversationId)
                    .NotEmpty().WithMessage("{PropertyName} is required")
                    .NotNull();
            RuleFor(x => x.BodyContent)
                    .NotEmpty().WithMessage("{PropertyName} is required")
                    .NotNull();
        }
    }
}
