using FluentValidation;
using GMarket.Domain.Entities;

namespace GMarket.Domain.Validators;

public class CartValidator : AbstractValidator<Cart>
{
    public CartValidator()
    {
        RuleFor(x=> x.User)
            .NotEmpty().WithMessage(ValidationMessage.Required);

        RuleFor(x => x.ProductItems).
            Must(x => x.Count < 100 & x.Count >= 0).WithMessage("ProductItems must be between 0 and 100.");

    }
}