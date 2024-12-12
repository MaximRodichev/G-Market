using GMarket.Domain.Entities.Market;

namespace GMarket.Domain.ViewModels;

public class ProductOptionsViewModel
{
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Image { get; set; }
    public string Category { get; set; }
    public List<string> tags { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
}