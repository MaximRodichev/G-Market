using GMarket.DAL.Interfaces;
using GMarket.DAL.Storage;
using GMarket.Domain.Entities.IdentityCustomer;
using GMarket.Domain.Entities.Market;
using GMarket.Service.Interfaces;
using GMarket.Service.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace GMarket;

public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseStorage<UserSecurity>, UserSecurityStorage>();
        services.AddScoped<IBaseStorage<CustomerContext>, CustomerContextStorage>();
        
        services.AddScoped<IBaseStorage<ProductItem>, ProductItemStorage>();
        services.AddScoped<IBaseStorage<Product>, ProductStorage>();
        services.AddScoped<IBaseStorage<ProductReview>, ProductReviewStorage>();
    }

    public static void InitializeServices(this IServiceCollection services, WebApplicationBuilder app)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        
        services
            .AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            })
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();
        
        services.AddRazorPages();
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = new PathString("/Home/Login");
                options.AccessDeniedPath = new PathString("/Home/AccessDenied");
            })
            .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            {
                options.ClientId = app.Configuration.GetSection("GoogleKeys:ClientId").Value;
                options.ClientSecret = app.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
                options.Scope.Add("profile");
                
                options.ClaimActions.MapJsonKey("picture", "picture");
            });
    }
}