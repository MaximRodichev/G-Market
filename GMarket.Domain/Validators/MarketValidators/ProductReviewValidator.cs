using FluentValidation;
using GMarket.Domain.Entities;
using GMarket.Domain.Entities.Market;

namespace GMarket.Domain.Validators.MarketValidators;

public class ProductReviewValidator : AbstractValidator<ProductReview>
{
    public ProductReviewValidator()
    {
        RuleFor(x => x.ProductItem)
            .NotEmpty().WithMessage(ValidationMessage.Required);
        
        RuleFor(x=>x.Mark)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessage.GreaterThanOrEqualTo)
            .LessThanOrEqualTo(5).WithMessage(ValidationMessage.LessThanOrEqualTo);

        RuleFor(x => x.ReviewText)
            .NotEmpty().WithMessage(ValidationMessage.Required);
    }
}