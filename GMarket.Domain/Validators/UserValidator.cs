using FluentValidation;
using GMarket.Domain.Entities;
using GMarket.Domain.Entities.IdentityCustomer;

namespace GMarket.Domain.Validators;

public class UserValidator : AbstractValidator<UserSecurity>
{
    public UserValidator()
    {
        
    }
}