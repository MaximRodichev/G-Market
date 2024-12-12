using AutoMapper;
using GMarket.Domain.FilterOptions;
using GMarket.Domain.ViewModels;
using GMarket.Service;
using GMarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace GMarket.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;
    
    private IMapper _mapper {get; set;}

    private MapperConfiguration _mapperConfiguration = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
        _mapper = _mapperConfiguration.CreateMapper();
    }

    public async Task<IActionResult> ListOfProducts()
    {
        var result = await _categoryService.GetAllProducts();
        return View(result.Data);
    }
    
    [HttpPost]
    public async Task<IActionResult> ListOfProductByFilter([FromBody] ProductItemFilterOptions options)
    {
        var result = await _categoryService.GetProductsByFilterOptions(options);
        return Ok(result.Data);
    }
    
    public IActionResult ListOfCategories()
    {
        List<CategoryView> categories = new List<CategoryView>()
        {
            new CategoryView()
            {
                CategoryDescription = "Основа любого огорода",
                CategoryImage = "/img/category_semena.jpg",
                CategoryName = "Семена"
            },
            new CategoryView()
            {
                CategoryDescription = "Лучшие помощники в быту",
                CategoryImage = "/img/category_inventory.jpg",
                CategoryName = "Инвентарь"
            },
            new CategoryView()
            {
                CategoryDescription = "Поможет вашему урожаю взрасти кратно",
                CategoryImage = "/img/category_ydobren.jpg",
                CategoryName = "Удобрения"
            }
        };
        return View(categories);
    }
}