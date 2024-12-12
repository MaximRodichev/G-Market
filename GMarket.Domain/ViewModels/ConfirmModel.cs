using System.ComponentModel.DataAnnotations;

namespace GMarket.Domain.ViewModels;

public class ConfirmModel : RegisterViewModel
{
    [Required(ErrorMessage = "Please enter verify code")]
    public string CodeConfirm { get; set; }
    [Required]
    public string GeneratedCode { get; set; }
}