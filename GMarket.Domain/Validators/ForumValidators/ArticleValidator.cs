using FluentValidation;
using GMarket.Domain.Entities.Forum;

namespace GMarket.Domain.Validators.ForumValidators;

public class ArticleValidator : AbstractValidator<Article>
{
    public ArticleValidator()
    {
        RuleFor(x=> x.AuthorName)
            .NotEmpty().WithMessage(ValidationMessage.Required);
        
        RuleFor(x=>x.Content)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .MaximumLength(10000).WithMessage(ValidationMessage.MaxValue);
        
        RuleFor(x=> x.Header)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .MaximumLength(100).WithMessage(ValidationMessage.MaxValue);

        RuleFor(x => x.FormattedContent)
            .NotEmpty().WithMessage(ValidationMessage.Required);

        RuleFor(x => x.User)
            .NotEmpty().WithMessage(ValidationMessage.Required);
    }
}