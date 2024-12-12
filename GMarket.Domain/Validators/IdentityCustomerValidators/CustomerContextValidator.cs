using FluentValidation;
using GMarket.Domain.Entities.IdentityCustomer;

namespace GMarket.Domain.Validators.IdentityCustomerValidators;

public class CustomerContextValidator : AbstractValidator<CustomerContext>
{
    public CustomerContextValidator()
    {
        RuleFor(x => x.UserSecurity)
            .NotEmpty().WithMessage(ValidationMessage.Required);
        
    }
}