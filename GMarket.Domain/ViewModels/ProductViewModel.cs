namespace GMarket.Domain.ViewModels;

public class ProductViewModel
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public string Name { get; set; }
    public string Brand {get; set;}
    public string Image { get; set; }
}