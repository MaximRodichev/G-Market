using System.Data;
using FluentValidation;
using GMarket.Domain.Entities;
using GMarket.Domain.Entities.IdentityCustomer;

namespace GMarket.Domain.Validators;

public class OrderItemValidator : AbstractValidator<OrderItem>
{
    public OrderItemValidator()
    {
        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .GreaterThanOrEqualTo(0).WithMessage(ValidationMessage.GreaterThanOrEqualTo);
        
        RuleFor(x => x.User)
            .NotEmpty().WithMessage(ValidationMessage.Required);

        RuleFor(x => x.Product)
            .NotEmpty().WithMessage(ValidationMessage.Required);
    }   
}