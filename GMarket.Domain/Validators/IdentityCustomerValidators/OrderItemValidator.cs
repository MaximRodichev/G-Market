using FluentValidation;
using GMarket.Domain.Entities.IdentityCustomer;

namespace GMarket.Domain.Validators.IdentityCustomerValidators;

public class OrderItemValidator : AbstractValidator<OrderItem>
{
    public OrderItemValidator()
    {
        RuleFor(x=>x.Product)
            .NotEmpty().WithMessage(ValidationMessage.Required);
        RuleFor(x=>x.Quantity)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .GreaterThanOrEqualTo(0).WithMessage(ValidationMessage.GreaterThanOrEqualTo);
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage(ValidationMessage.Required);
        RuleFor(x=>x.BuyPrice)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .GreaterThanOrEqualTo(0).WithMessage(ValidationMessage.GreaterThanOrEqualTo);
        
    }
}