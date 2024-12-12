using System.Data;
using FluentValidation;
using GMarket.Domain.Entities;
using GMarket.Domain.Entities.Market;

namespace GMarket.Domain.Validators.MarketValidators;

public class ProductItemValidator : AbstractValidator<ProductItem>
{
    public ProductItemValidator()
    {
        RuleFor(x=>x.ProductId)
            .NotEmpty().WithMessage(ValidationMessage.Required);
        
        RuleFor(x=>x.Product)
            .NotEmpty().WithMessage(ValidationMessage.Required);
        
        RuleFor(x=>x.Brand)
            .NotEmpty().WithMessage(ValidationMessage.Required);
        
        RuleFor(x=>x.Rating)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .LessThanOrEqualTo(5).WithMessage(ValidationMessage.LessThanOrEqualTo)
            .GreaterThanOrEqualTo(1).WithMessage(ValidationMessage.GreaterThanOrEqualTo);
        
        RuleFor(x=>x.Category)
            .NotEmpty().WithMessage(ValidationMessage.Required);
        
        RuleFor(x=>x.Description)
            .NotEmpty().WithMessage(ValidationMessage.Required);
    }
}