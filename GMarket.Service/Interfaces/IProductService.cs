using GMarket.Domain.Entities.Market;
using GMarket.Domain.Response;
using GMarket.Domain.ViewModels;

namespace GMarket.Service.Interfaces;

public interface IProductService
{
    public Task<BaseResponse<Product>> CreateProduct(ProductOptionsViewModel product);
}