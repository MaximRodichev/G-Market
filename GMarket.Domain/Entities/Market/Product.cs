using GMarket.Domain.Validators.MarketValidators;

namespace GMarket.Domain.Entities.Market;

/// <summary>
/// Сущность содержащая основные данные о товаре
/// </summary>
public class Product : BaseEntity<Product>
{
    /// <summary>
    /// Базовый конструктор для EFCore
    /// </summary>
    public Product()
    {
        
    }
    
    /// <summary>
    /// Базовый конструктор
    /// </summary>
    /// <param name="name"></param>
    /// <param name="quantity"></param>
    /// <param name="price"></param>
    public Product(string name, int quantity, decimal price, ProductItem productItem)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
        ProductItem = productItem;
        
        ValidateEntity(new ProductValidator());
    }
    
    
    /// <summary>
    /// Название продукта
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Количество на складе
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Цена
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Навигация к описанию товара
    /// </summary>
    public ProductItem ProductItem { get; set; }
    
    public Guid ProductItemId { get; set; }
}
