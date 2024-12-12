using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GMarket.Domain.Types;
using GMarket.Domain.Validators.IdentityCustomerValidators;

namespace GMarket.Domain.Entities.IdentityCustomer;

[Table("UserSecurity")]
public class UserSecurity : BaseEntity<UserSecurity>
{
    /// <summary>
    /// Конструктор для EFCore
    /// </summary>
    public UserSecurity()
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
    public UserSecurity(string password, string email)
    {
        Password = password;
        Email = email;
        Role = Role.User;  // Инициализация списка ролей, если он null
        
        ValidateEntity(new UserSecurityValidator());
    }
    
    /// <summary>
    /// Почта пользователя (логин)
    /// </summary>
    [Column("Email")]
    public string Email { get; init; }
    
    /// <summary>
    /// Пароль пользователя в sha256
    /// </summary>
    [Column("Password")]
    public string Password { get; private set; }

    /// <summary>
    /// Роль пользователя: Администратор, Пользователей, Контент-Мейкер
    /// </summary>
    [Column("Role")]
    public Role Role { get; private set; } = Role.User;
    
    /// <summary>
    /// Навигация по контексту пользователя
    /// </summary>
    public CustomerContext Context { get; set; }
    

    #region Methods

    public void SetRole(Role role)
    {
        Role = role;
    }
    
    #endregion
    
}