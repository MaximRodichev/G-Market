using AutoMapper;
using GMarket.DAL.Interfaces;
using GMarket.Domain.Entities.Market;
using GMarket.Domain.Response;
using GMarket.Domain.Types;
using GMarket.Domain.ViewModels;
using GMarket.Service.Interfaces;

namespace GMarket.Service.Services;

public class ProductService : IProductService
{
    private readonly IBaseStorage<ProductItem> _productItemStorage;
    private readonly IBaseStorage<Product> _productStorage;
    private readonly IBaseStorage<ProductReview> _productReviewStorage;
    
    private IMapper _mapper { get; set; }

    private MapperConfiguration mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public ProductService(IBaseStorage<ProductItem> productItemStorage, IBaseStorage<Product> productStorage, IBaseStorage<ProductReview> productReviewStorage)
    {
        _productItemStorage = productItemStorage;
        _productStorage = productStorage;
        _productReviewStorage = productReviewStorage;
        _mapper = mapperConfiguration.CreateMapper();
    }


    public async Task<BaseResponse<Product>> CreateProduct(ProductOptionsViewModel product)
    {
        try
        {
            Product productModel = _mapper.Map<Product>(product);

            var productDb = await _productStorage.AddAsync(productModel);
        
            try
            {
                ProductItem productItemModel = _mapper.Map<ProductItem>(product);
                productItemModel.ProductId = productDb.Id;
                productItemModel.Images = new List<string>() { product.Image };

                var productItemDb = await _productItemStorage.AddAsync(productItemModel);
                productDb.ProductItemId = productItemDb.Id;
                await _productStorage.UpdateAsync(productDb);
                return new BaseResponse<Product>()
                {
                    Data = productDb,
                    Description = "Success",
                    StatusCode = StatusCode.Ok,
                };
            }
            catch (Exception ex2)
            {
                await _productStorage.DeleteAsync(productDb);

                return new BaseResponse<Product>()
                {
                    Description = ex2.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }
        catch (Exception ex)
        {
            return new BaseResponse<Product>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}