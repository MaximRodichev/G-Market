using GMarket.Domain.Entities.IdentityCustomer;

namespace GMarket.Domain.Entities.Forum;

public class Article : BaseEntity<Article>
{

    /// <summary>
    /// Базовый конструктор для EFCore
    /// </summary>
    public Article()
    {
        
    }

    public Article(CustomerContext user,
        string authorName,
        string header,
        string content,
        string formattedContent,
        ICollection<string> tags)
    {
        User = user;
        AuthorName = user.UserName;
        Header = header;
        Content = content;
        FormattedContent = formattedContent;
        Tags = tags;
    }
    
    /// <summary>
    /// Навигация к автору
    /// </summary>
    public CustomerContext User { get; set; }
    
    /// <summary>
    /// Имя автора статьи
    /// </summary>
    public string AuthorName { get; set; }
    
    /// <summary>
    /// Шапка заголовок статьи
    /// </summary>
    public string Header { get; set; }
    
    /// <summary>
    /// Неформатированный контент
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Форматированный контент
    /// </summary>
    public string FormattedContent { get; set; }
    
    /// <summary>
    /// Навигация по Комментариям статьи
    /// </summary>
    public ICollection<ArticleCommentary> ArticleCommentaries { get; set; } = new List<ArticleCommentary>();
    
    /// <summary>
    /// Картинки из статьи
    /// </summary>
    public ICollection<string> s3Images { get; set; } = new List<string>();
    
    /// <summary>
    /// Теги для поиска
    /// </summary>
    public ICollection<string> Tags { get; set; } = new List<string>();
    
    /// <summary>
    /// Дата создания статьи
    /// </summary>
    public DateTime DateCreated { get; set; } = DateTime.Now;
}