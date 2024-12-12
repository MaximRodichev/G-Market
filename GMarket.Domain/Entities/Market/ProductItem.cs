using System.ComponentModel.DataAnnotations.Schema;
using GMarket.Domain.Validators.MarketValidators;

namespace GMarket.Domain.Entities.Market;

/// <summary>
/// Сущность реализующая позицию товара, рейтинг отзывы, прайс.
/// </summary>
public class ProductItem : BaseEntity<ProductItem>
{
    /// <summary>
    /// Базовый конструктор для EFCore
    /// </summary>
    public ProductItem()
    {
        
    }

    public ProductItem(Product product, 
        string category,
        string description,
        string brand,
        ICollection<string> images,
        ICollection<string> tags)
    {
        Product = product;
        Category = category;
        Description = description;
        Brand = brand;
        Images = images;
        Tags = tags;
        
        ValidateEntity(new ProductItemValidator());
    }
    
    /// <summary>
    /// Навигация к продукту
    /// </summary>
    public Product Product { get; set; }
    [ForeignKey("ProductId")]
    public Guid ProductId { get; set; }
    
    /// <summary>
    /// Категория товара
    /// </summary>
    public string Category { get; set; }
    
    /// <summary>
    /// Описание товара
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Бренд
    /// </summary>
    public string Brand { get; set; }
    
    /// <summary>
    /// Теги для поиска
    /// </summary>
    public ICollection<string> Tags { get; set; } = new List<string>();
    
    /// <summary>
    /// Рейтинг товара
    /// </summary>
    public int Rating { get; private set; }

    /// <summary>
    /// Ссылка на лист изображений в S3 хранилище YandexObjectStorage
    /// </summary>
    public ICollection<string>? Images { get; set; } = new List<string>();
    
    /// <summary>
    /// Лист с комментариями к позиции товара
    /// </summary>
    public ICollection<ProductReview>? Reviews { get; set; } = new List<ProductReview>();
}