using GMarket.Domain.Validators;

namespace GMarket.Domain.Entities;

/// <summary>
/// Сущность Корзины пользователя, хранит ссылки на товары
/// </summary>
public class Cart : BaseEntity<Cart>
{
    /// <summary>
    /// Конструктор для EFCore
    /// </summary>
    public Cart(){}

    /// <summary>
    /// Базовый конструктор
    /// </summary>
    /// <param name="user">Навигационное свойство</param>
    public Cart(User user)
    {
        ProductItems = new List<ProductItem>();
        User = user;
        
        ValidateEntity(new CartValidator());
    }
    
    public ICollection<ProductItem> ProductItems { get; private set; }
    public User User { get; private init; }

    #region Methods
    
    /// <summary>
    /// Добавляет новый товар в корзину
    /// </summary>
    /// <param name="productItem">Товар</param>
    /// <exception cref="ArgumentNullException">Ошибка по нулевой ссылке на товар</exception>
    /// <exception cref="ArgumentException">Попытка добавить уже существующий товар</exception>
    public void AddProductItem(ProductItem productItem)
    {
        if(productItem == null) throw new ArgumentNullException();
        if(ProductItems.Any(p => p.Id == productItem.Id)) throw new ArgumentException();
        ProductItems.Add(productItem);
        
        ValidateEntity(new CartValidator());
    }
    
    /// <summary>
    /// Удаляет элемент из корзины
    /// </summary>
    /// <param name="productItem">Добавляемый товар</param>
    /// <exception cref="ArgumentNullException">Ошибка по нулевой ссылке на товар</exception>
    /// <exception cref="ArgumentException">Попытка удалить несуществующий элемент</exception>
    public void RemoveProductItem(ProductItem productItem)
    {
        if(productItem == null) throw new ArgumentNullException();
        if(ProductItems.Any(p => p.Id == productItem.Id)) ProductItems.Remove(productItem);
        else throw new ArgumentException();
        
        ValidateEntity(new CartValidator());   
    }

    #endregion
    
}