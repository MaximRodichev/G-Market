using System.ComponentModel.DataAnnotations;

namespace GMarket.Domain.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Введите почту")]
    [EmailAddress(ErrorMessage = "Некорректны адрес электроной почты")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "Пароль не должен иметь длину больше 6 символов")]
    [Display(Name= "Пароль")]
    public string Password { get; set; }
}