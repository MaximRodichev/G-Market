using FluentValidation;
using GMarket.Domain.Entities;
using GMarket.Domain;
using GMarket.Domain.Entities.Market;

namespace GMarket.Domain.Validators.MarketValidators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x=>x.Quantity)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .GreaterThanOrEqualTo(0).WithMessage(ValidationMessage.GreaterThanOrEqualTo)
            .LessThanOrEqualTo(100).WithMessage(ValidationMessage.LessThanOrEqualTo);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessage.Required);

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .GreaterThanOrEqualTo(0).WithMessage(ValidationMessage.GreaterThanOrEqualTo);
    }
}