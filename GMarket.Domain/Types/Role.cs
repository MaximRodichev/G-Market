using System.ComponentModel.DataAnnotations;

namespace GMarket.Domain.Types;

public enum Role
{
    [Display(Name = "Пользователь")]
    User,
    [Display(Name = "Контент-мейкер")]
    ContentMaker,
    [Display(Name = "Администратор")]
    Admin
}