using System.ComponentModel.DataAnnotations;

namespace GMarket.Domain.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Username is required")]
    [MinLength(3, ErrorMessage = "Имя должно иметь длину не меньше 3 символов")]
    [MaxLength(20, ErrorMessage = "Имя должно иметь длину не более 20 символов")]
    public string Login { get; set; }
    
    [EmailAddress(ErrorMessage = "Некорректный адрес электронной почты")]
    [Required(ErrorMessage = "Укажите почту")]
    public string Email { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Укажите пароль")]
    [MinLength(6, ErrorMessage = "Пароль не должен иметь длину больше 6 символов")]
    public string Password { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Подтвердите пароль")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; }
}