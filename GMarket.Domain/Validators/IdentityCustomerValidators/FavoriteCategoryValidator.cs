using FluentValidation;
using GMarket.Domain.Entities.IdentityCustomer;

namespace GMarket.Domain.Validators.IdentityCustomerValidators;

public class FavoriteCategoryValidator : AbstractValidator<FavoriteCategory>
{
    public FavoriteCategoryValidator()
    {
        RuleFor(x => x.User)
            .NotNull().WithMessage(ValidationMessage.Required);
        
        RuleFor(x=> x.CategoryName)
            .NotNull().WithMessage(ValidationMessage.Required)
            .Matches(@"^[a-zA-Zа-яА-ЯёЁ]+$").WithMessage(ValidationMessage.OnlyLetters);
        
        
    }
}