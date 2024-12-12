using FluentValidation;
using GMarket.Domain.Entities.IdentityCustomer;

namespace GMarket.Domain.Validators.IdentityCustomerValidators;

public class UserSecurityValidator : AbstractValidator<UserSecurity>
{
    public UserSecurityValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage(ValidationMessage.EmailInvalid);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(ValidationMessage.Required);
    }
}