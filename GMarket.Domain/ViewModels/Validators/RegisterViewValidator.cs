using FluentValidation;
using FluentValidation.Validators;

namespace GMarket.Domain.ViewModels.Validators;

public class RegisterViewValidator : AbstractValidator<RegisterViewModel>
{
    public RegisterViewValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .EmailAddress().WithMessage(ValidationMessage.EmailInvalid);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .MinimumLength(6).WithMessage(ValidationMessage.MinValue)
            .MaximumLength(15).WithMessage(ValidationMessage.MaxValue);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .Equal(x => x.Password).WithMessage(ValidationMessage.NotEqual);
    }
}