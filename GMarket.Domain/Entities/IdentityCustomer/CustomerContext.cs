using System.ComponentModel.DataAnnotations.Schema;
using GMarket.Domain.Entities.Forum;
using GMarket.Domain.Entities.Market;
using GMarket.Domain.Validators.IdentityCustomerValidators;
using GMarket.Domain.ValueObjects;

namespace GMarket.Domain.Entities.IdentityCustomer;

public class CustomerContext : BaseEntity<CustomerContext>
{
    /// <summary>
    /// Базовый конструктор для EFCore
    /// </summary>
    public CustomerContext()
    {
        
    }

    public CustomerContext(string username, string fullName, UserSecurity user)
    {
        UserName = username;
        FullName = fullName;
        UserSecurity = user;
        UserSecurityId = user.Id;
        
        ValidateEntity(new CustomerContextValidator());
    }
    
    public Guid UserSecurityId { get; set; }
    public UserSecurity UserSecurity { get; set; }
    
    /// <summary>
    /// Юзернейм
    /// </summary>
    public string UserName { get; set; }
    
    /// <summary>
    /// Полное имя пользователя
    /// </summary>
    public string FullName { get; set; }
    
    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public ICollection<FavoriteCategory>? FavoritesCategories { get; set; } = new List<FavoriteCategory>();
    public ICollection<Article>? Articles { get; set; } = new List<Article>();
    public ICollection<ArticleCommentary>? ArticleCommentaries { get; set; } = new List<ArticleCommentary>();
    public ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<ProductReview>? ProductReviews { get; set; } = new List<ProductReview>();

    /// <summary>
    /// Изображение профиля
    /// </summary>
    public string S3Image { get; set; } = "./img/user.svg";
}