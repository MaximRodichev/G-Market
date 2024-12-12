using AutoMapper;
using GMarket.Domain.ViewModels;
using GMarket.Service;
using GMarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GMarket.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    
    private IMapper _mapper {get; set;}
    
    private MapperConfiguration _mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public ProductController(IProductService categoryService)
    {
        _productService = categoryService;
        _mapper = _mapperConfiguration.CreateMapper();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductOptionsViewModel model)
    {
        var response = await _productService.CreateProduct(model);
        return Ok(response);
    }
}
