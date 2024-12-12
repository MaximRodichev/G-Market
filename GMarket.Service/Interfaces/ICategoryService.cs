using GMarket.Domain.Entities.Market;
using GMarket.Domain.FilterOptions;
using GMarket.Domain.Response;
using GMarket.Domain.ViewModels;

namespace GMarket.Service.Interfaces;

public interface ICategoryService
{
    public Task<BaseResponse<List<ProductViewModel>>> GetAllProducts();

    public Task<BaseResponse<List<ProductViewModel>>> GetProductsByFilterOptions(ProductItemFilterOptions filterOptions);
    
    
}