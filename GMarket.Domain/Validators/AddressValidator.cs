using FluentValidation;
using GMarket.Domain.ValueObjects;

namespace GMarket.Domain.Validators;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .Matches(@"^([A-Za-z]+|[А-Яа-я]+)$").WithMessage(ValidationMessage.OnlyLetters);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .Matches(@"^([A-Za-z]+|[А-Яа-я]+)$").WithMessage(ValidationMessage.OnlyLetters);
        
        RuleFor(x=>x.Surname)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .Matches(@"^([A-Za-z]+|[А-Яа-я]+)$").WithMessage(ValidationMessage.OnlyLetters);
        
        RuleFor(x=>x.House)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .Matches(@"^(?!0)[0-9]{1,3}$").WithMessage($"{ValidationMessage.OnlyNumbers}\n Номер дома может быть от 1 до 999");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .Matches(@"^77[0-9]{6}$").WithMessage(ValidationMessage.PhoneNumber);

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .Matches(@"^([A-Za-z]+|[А-Яа-я]+)$").WithMessage(ValidationMessage.OnlyLetters);
        
        RuleFor(x=>x.PostCode)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .Matches(@"^(?!0)[0-9]{4}$").WithMessage(ValidationMessage.OnlyNumbers);

    }
}