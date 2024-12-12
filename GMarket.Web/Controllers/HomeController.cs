using System.Diagnostics;
using System.Security.Claims;
using AutoMapper;
using GMarket.Domain.Entities.IdentityCustomer;
using GMarket.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using GMarket.Models;
using GMarket.Service;
using GMarket.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GMarket.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    private readonly IAccountService _accountService;

    private IMapper _mapper { get; set; }

    private MapperConfiguration _mapperConfig = new MapperConfiguration(p =>
    {
        p.AddProfile(new AppMappingProfile());
    });
    
    private IWebHostEnvironment _appEnvironment;
    public HomeController(ILogger<HomeController> logger, IAccountService accountService, IWebHostEnvironment appEnvironment)
    {
        _logger = logger;
        _accountService = accountService;
        _mapper = _mapperConfig.CreateMapper();
        _appEnvironment = appEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var response = await _accountService.Login(model);
            if (response.StatusCode == Domain.Types.StatusCode.Ok)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(response.Data));
                return Ok(response);
            }
            
            ModelState.AddModelError("", response.Description);
        }
        var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        return BadRequest(errors);
    }
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {   
            var responseConfirmCode = await _accountService.Register(model);
            
            var confirmModel = _mapper.Map<ConfirmModel>(model);

            confirmModel.GeneratedCode = responseConfirmCode.Data;
            
            return Ok(confirmModel);
        }
        var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
            
        return BadRequest(errors);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return RedirectToAction("Index", "Home");
    }

    public async Task AuthenticationGoogle(string returnUrl = "/")
    {
        await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
            new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse", new { returnUrl }),
                Parameters = { { "prompt", "select_account" } }
            });
    }

    public async Task<IActionResult> GoogleResponse(string returnUrl = "/")
    {
        try
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result.Succeeded == true)
            {
                var response = await _accountService.IsCreatedAccount(new GoogleOAuth()
                {
                    Email = result.Principal.FindFirst(ClaimTypes.Email)?.Value,
                    Login = result.Principal.FindFirst(ClaimTypes.Name)?.Value,
                    Name = result.Principal.FindFirst(ClaimTypes.Surname)?.Value,
                    Image = "/" +
                        SaveImageInImageUser(result.Principal.FindFirst("picture")?.Value, result)
                            .Result ?? "img/user.svg"
                });

                if (response.StatusCode == Domain.Types.StatusCode.Ok)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));
                    return Redirect(returnUrl);
                }
            }

            return BadRequest("Failed to authenticate");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    public async Task<string> SaveImageInImageUser(string imageUrl, AuthenticateResult result)
    {
        string filePath = "";
        if (!string.IsNullOrEmpty(imageUrl))
        {
            using (var httpClient = new HttpClient())
            {
                filePath = Path.Combine("ImageUser", $"{result.Principal.FindFirst(ClaimTypes.Email)?.Value}-avatar.jpg");
                
                var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);
                
                await System.IO.File.WriteAllBytesAsync(Path.Combine(_appEnvironment.WebRootPath, filePath), imageBytes);
            }
        }
        return filePath;
    }
    
    [HttpPost]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmModel model)
    {
        var registerViewModel = _mapper.Map<RegisterViewModel>(model);
        var response = await _accountService.ConfirmEmail(registerViewModel, model.GeneratedCode, model.CodeConfirm);
        if (response.StatusCode == Domain.Types.StatusCode.Ok)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(response.Data));

            return Ok(model);
        }
        ModelState.AddModelError("", response.Description);
        
        var errors = ModelState.Values.SelectMany(v=>v.Errors)
            .Select(e => e.ErrorMessage).ToList();
        
        return BadRequest(errors);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}