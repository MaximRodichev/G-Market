using GMarket.Domain.Entities.IdentityCustomer;

namespace GMarket.Domain.Entities.Forum;

public class ArticleCommentary : BaseEntity<ArticleCommentary>
{
    
    /// <summary>
    /// Базовый конструктор для EFCore
    /// </summary>
    public ArticleCommentary()
    {
        
    }

    public ArticleCommentary(CustomerContext user, Article article, string commentaryText, ArticleCommentary? articleCommentary)
    {
        User = user;
        Article = article;
        CommentaryText = commentaryText;
        AuthorName = user.UserName;
        ReplyToCommentary = articleCommentary;
    }
    
    /// <summary>
    /// Навигация по автору статьи
    /// </summary>
    public CustomerContext User { get; init; }
    /// <summary>
    /// Навигация к статьие
    /// </summary>
    public Article Article { get; init; }
    /// <summary>
    /// Имя автора комментария
    /// </summary>
    public string AuthorName { get; init; }
    /// <summary>
    /// Текст комментария
    /// </summary>
    public string CommentaryText { get; init; }
    /// <summary>
    /// Куда ответили?
    /// </summary>
    public ArticleCommentary? ReplyToCommentary { get; init; }
}