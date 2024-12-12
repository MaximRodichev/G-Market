namespace GMarket.Domain.FilterOptions;

public class ProductItemFilterOptions
{
    public  List<string> Brands { get; set; }
    
    public decimal PriceMax { get; set; }
    
    public decimal PriceMin { get; set; }
}