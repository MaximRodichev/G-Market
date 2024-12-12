using FluentValidation;

namespace GMarket.Domain.ViewModels.Validators;

public class LoginViewValidator : AbstractValidator<LoginViewModel>
{
    public LoginViewValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .EmailAddress().WithMessage(ValidationMessage.EmailInvalid);
        RuleFor(x => x.Password)
            .MinimumLength(8).WithMessage(ValidationMessage.MinValue);
    }
    
}