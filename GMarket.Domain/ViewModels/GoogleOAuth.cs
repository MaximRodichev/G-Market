using System.ComponentModel.DataAnnotations;

namespace GMarket.Domain.ViewModels;

public class GoogleOAuth
{
    [EmailAddress]
    public string Email { get; set; }
    
    
    public string Login { get; set; }
    
    
    public string Image { get; set; }
    
    public string Name { get; set; }
}