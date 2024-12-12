using GMarket.Domain.Validators;

namespace GMarket.Domain.Entities;

public class User : BaseEntity<User>
{
    /// <summary>
    /// Конструктор для EFCore
    /// </summary>
    public User()
    {
        
    }
    
    /// <summary>
    /// Конструктор для инициализации нового пользователя с одной ролью
    /// </summary>
    /// <param name="name">Имя</param>
    /// <param name="surname">Фамилия</param>
    /// <param name="password">Пароль (ХЭШ)</param>
    /// <param name="email">Почта</param>
    /// <param name="role">Роль</param>
    public User( string password, string email, string role)
        : this( password, email, new List<string> { role })
    {
    }

    /// <summary>
    /// Конструктор для инициализации нового пользователя с несколькими ролями
    /// </summary>
    /// <param name="name">Имя</param>
    /// <param name="surname">Фамилия</param>
    /// <param name="password">Пароль (ХЭШ)</param>
    /// <param name="email">Почта</param>
    /// <param name="roles">Список ролей</param>
    public User(string password, string email, ICollection<string> roles)
    {
        Password = password;
        Email = email;
        Roles = roles ?? new List<string>();  // Инициализация списка ролей, если он null
        Cart = new Cart(this);
        
        ValidateEntity(new UserValidator());
    }
    
    /// <summary>
    /// Почта пользователя (логин)
    /// </summary>
    public string Email { get; init; }
    
    /// <summary>
    /// Пароль пользователя в sha256
    /// </summary>
    public string Password { get; private set; }
    
    /// <summary>
    /// Время создания аккаунта
    /// </summary>
    public DateTime CreatedAt { get; private init; } = DateTime.UtcNow;
    
    /// <summary>
    /// Роль пользователя: Администратор, Пользователей, Контент-Мейкер
    /// </summary>
    public ICollection<string> Roles { get; private set; } 
    
    /// <summary>
    /// Список заказов
    /// </summary>
    public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();
    
    /// <summary>
    /// Навигационное свойство по отзывам на товары от пользователя
    /// </summary>
    public ICollection<ProductReview> ProductReviews { get; private set; } = new List<ProductReview>();
    
    /// <summary>
    /// Список товаров в корзине
    /// </summary>
    public Cart Cart { get; private set; }

    #region Methods
    
    /// <summary>
    /// Смена пароля пользователя 
    /// </summary>
    /// <param name="password"></param>
    public void ChangePassword(string password)
    {
        Password = password;
        
        ValidateEntity(new UserValidator());
    }
    
    /// <summary>
    /// Метод удаления роли пользователя
    /// </summary>
    /// <param name="role">Роль</param>
    /// <exception cref="ArgumentException">Несуществующая роль</exception>
    public void RemoveRoles(string role)
    {
        if(!Roles.Contains(role)) throw new ArgumentException("Role does not exist");
        Roles.Remove(role);
    }
    
    /// <summary>
    /// Метод добавления новой роли пользователю
    /// </summary>
    /// <param name="role">Роль</param>
    /// <exception cref="ArgumentException">Уже существует роль</exception>
    public void AddRole(string role)
    {
        if(Roles.Contains(role)) throw new ArgumentException("Role already exists");
        Roles.Add(role);
    }
    
    /// <summary>
    /// Добавление нового заказа
    /// </summary>
    /// <param name="orderItem"></param>
    public void AddOrderItem(OrderItem orderItem)
    {
        if (orderItem.User == this)
        {
            OrderItems.Add(orderItem);
        }
        else
        {
            throw new Exception();
            
        }
        
        ValidateEntity(new UserValidator());
    }

    /// <summary>
    /// Добавление товара в корзину
    /// </summary>
    /// <param name="productItem">Добавление товара в корзину</param>
    public void AddCartItem(ProductItem productItem)
    {
        Cart.AddProductItem(productItem);
        
        ValidateEntity(new UserValidator());
    }
    
    /// <summary>
    /// Метод отмены заказа
    /// </summary>
    /// <param name="orderItem">Заказ</param>
    public void CancelOrderItem(OrderItem orderItem)
    {
        if (OrderItems.Contains(orderItem) && orderItem.User == this)
        {
            orderItem.CancelOrder();
        }
    }

    /// <summary>
    /// Метод удаления товара из корзины
    /// </summary>
    /// <param name="productItem">Позиция в корзине</param>
    public void RemoveCartItem(ProductItem productItem)
    {
        Cart.RemoveProductItem(productItem);
        
        ValidateEntity(new UserValidator());
    }
    
    
    
    
    #endregion
    
}