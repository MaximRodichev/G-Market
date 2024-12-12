using FluentValidation;
using GMarket.Domain.Entities.Forum;

namespace GMarket.Domain.Validators.ForumValidators;

public class ArticleCommentaryValidator : AbstractValidator<ArticleCommentary>
{
    public ArticleCommentaryValidator()
    {
        RuleFor(x => x)
            .Must(x =>x.User.UserName == x.AuthorName)
            .WithMessage("AuthorName is not equals name of user");
        RuleFor(x=>x.User)
            .NotNull().NotEmpty().WithMessage(ValidationMessage.Required);
        RuleFor(x=>x.Article)
            .NotNull().NotEmpty().WithMessage(ValidationMessage.Required);
        RuleFor(x => x.AuthorName)
            .NotNull().NotEmpty().WithMessage(ValidationMessage.Required);
        RuleFor(x=>x.CommentaryText)
            .NotNull().NotEmpty().WithMessage(ValidationMessage.Required);
    }
}