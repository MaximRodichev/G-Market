using GMarket.Domain.Entities.IdentityCustomer;
using GMarket.Domain.Validators;
using GMarket.Domain.Validators.MarketValidators;

namespace GMarket.Domain.Entities.Market;

public class ProductReview : BaseEntity<ProductReview>
{
    /// <summary>
    /// Конструктор для EFCore
    /// </summary>
    public ProductReview()
    {
        
    }

    public ProductReview(ProductItem productItem, CustomerContext user, int mark, string reviewText)
    {
        ProductItem = productItem;
        User = user;
        Mark = mark;
        ReviewText = reviewText;
        
        ValidateEntity(new ProductReviewValidator());
    }
    
    
    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public ProductItem ProductItem { get; private init; }
    
    /// <summary>
    /// Навигационное свойство, идентифицирующее, кто написал отзыв
    /// </summary>
    public CustomerContext User { get; private init; }

    /// <summary>
    /// Оценка 1-5 звезд
    /// </summary>
    public int Mark { get; private init; }

    /// <summary>
    /// Текст отзыва
    /// </summary>
    public string ReviewText { get; private init; }
    
    /// <summary>
    /// Ответ продавца, он есть.
    /// </summary>
    public string? ReplyText { get; private set; }
    
    /// <summary>
    /// Момент создания отзыва
    /// </summary>
    public DateTime CreatedAt { get; private init; } = DateTime.Now;
    
    /// <summary>
    /// Изображения, если они есть. (Фото товара)
    /// </summary>
    public ICollection<string>? Images { get; private init; }

    #region Methods

    /// <summary>
    /// Добавление ответа на комментарий от Продавца
    /// </summary>
    /// <param name="text">Текст ответа</param>
    public void AddReplyText(string text)
    {
        ReplyText = text;
        
        ValidateEntity(new ProductReviewValidator());
    }

    #endregion
}