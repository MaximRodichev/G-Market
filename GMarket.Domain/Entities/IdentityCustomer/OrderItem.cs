using GMarket.Domain.Entities.IdentityCustomer;
using GMarket.Domain.Entities.Market;
using GMarket.Domain.Types;
using GMarket.Domain.Validators;
using GMarket.Domain.ValueObjects;

namespace GMarket.Domain.Entities.IdentityCustomer;

public class OrderItem : BaseEntity<OrderItem>
{
    /// <summary>
    /// Конструктор для EFCore
    /// </summary>
    public OrderItem() { }


    public OrderItem(Product product, CustomerContext user, int quantity, decimal buyPrice, Address address)
    {
        Product = product;
        User = user;
        Quantity = quantity;
        BuyPrice = buyPrice;
        Address = address;
        
    }
    
    /// <summary>
    /// Навигационное свойство для нахождения свойств продукта
    /// </summary>
    public Product Product { get; private init; }
    
    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public CustomerContext User { get; private init; }
    
    /// <summary>
    /// Количество товаров в заказе
    /// </summary>
    public int Quantity { get; private set; }
    
    /// <summary>
    /// Цена покупки
    /// </summary>
    public decimal BuyPrice { get; private set; }
    
    /// <summary>
    /// Информация куда доставлять
    /// </summary>
    public Address Address { get; private set; }
    
    /// <summary>
    /// Время создания заказа
    /// </summary>
    public DateTime CreatedAt { get; private init; } = DateTime.Now;

    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get; private set; } = OrderStatus.AwaitingPayment;

    /// <summary>
    /// Трек код для отслеживания заказа
    /// </summary>
    public string TrackCode { get; private set; } = "";

    #region Methods
    
    /// <summary>
    /// Отмена заказа пользователем
    /// </summary>
    /// <exception cref="Exception">Товар уже в пути или был доставлен</exception>
    public void CancelOrder()
    {
        if (Status == OrderStatus.AwaitingPayment || Status == OrderStatus.Pending)
        {
            Status = OrderStatus.Canceled;
        }
        else
        {
            throw new Exception("Order cannot be cancelled");
        }
    }

    public void SetTrackCode(string trackCode)
    {
        if(trackCode == null){throw new ArgumentNullException(nameof(trackCode));}
        
        if (string.IsNullOrWhiteSpace(TrackCode))
        {
            TrackCode = trackCode;
        }
        else
        {
            throw new Exception("Track code cannot be changed");
        }
    }

    #endregion
}