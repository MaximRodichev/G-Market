using AutoMapper;
using GMarket.DAL.Interfaces;
using GMarket.Domain.Entities.Market;
using GMarket.Domain.FilterOptions;
using GMarket.Domain.Response;
using GMarket.Domain.Types;
using GMarket.Domain.ViewModels;
using GMarket.Service.Interfaces;
using Exception = System.Exception;

namespace GMarket.Service.Services;

public class CategoryService : ICategoryService
{
    private readonly IBaseStorage<ProductItem> _productItemStorage;
    private readonly IBaseStorage<Product> _productStorage;
    
    private IMapper _mapper { get; set; }

    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public CategoryService(IBaseStorage<ProductItem> productItemStorage, IBaseStorage<Product> productStorage)
    {
        _productStorage = productStorage;
        _productItemStorage = productItemStorage;
        _mapper = mapperConfiguration.CreateMapper();
    }
    
    public async Task<BaseResponse<List<ProductViewModel>>> GetAllProducts()
    {
        try
        {
            var products = _productItemStorage.GetAllAsync().ToList();
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            foreach (var product in products)
            {
                Product productInfo = await _productStorage.GetByIdAsync(product.ProductId);
                productViewModels.Add(
                    new ProductViewModel()
                    {
                        Id = product.Id,
                        Brand = product.Brand,
                        Category = product.Category,
                        Image = product.Images.ToArray()[0],
                        Name = productInfo.Name,
                        Price = productInfo.Price,
                    });
            }
            
            if (products.Count == 0)
            {
                return new BaseResponse<List<ProductViewModel>>()
                {
                    Data = new List<ProductViewModel>(),
                    Description = "No products found",
                    StatusCode = StatusCode.Ok
                };
            }

            return new BaseResponse<List<ProductViewModel>>()
            {
                Data = productViewModels,
                Description = "Products found",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<ProductViewModel>>()
            {
                Data = new List<ProductViewModel>(),
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
        
    }

    public async Task<BaseResponse<List<ProductViewModel>>> GetProductsByFilterOptions(ProductItemFilterOptions filters)
    {
        try
        {
            var response = await GetAllProducts();
            var products = response.Data;

            if (filters.Brands != null && filters.Brands.Count > 0)
            {
                products = products.Where(x=> filters.Brands.Contains(x.Brand)).ToList();
            }
    
            if (filters.PriceMin > 0 && filters.PriceMin < 1000 && filters.PriceMin < filters.PriceMax)
            {
                products = products.Where(x => x.Price < filters.PriceMax).ToList();
            }

            if (filters.PriceMax > 0 && filters.PriceMax < 5000)
            {
                products =products.Where(x => x.Price < filters.PriceMax).ToList();
            }

            return new BaseResponse<List<ProductViewModel>>()
            {
                Data = products,
                Description = "Success",
                StatusCode = StatusCode.Ok
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<ProductViewModel>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
        return null;
    }
}