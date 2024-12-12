using System.Security.Claims;
using GMarket.Domain.Entities.IdentityCustomer;
using GMarket.Domain.Response;
using GMarket.Domain.ViewModels;

namespace GMarket.Service.Interfaces;

public interface IAccountService
{
    Task<BaseResponse<string>> Register(RegisterViewModel model);
    
    Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
    
    Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(RegisterViewModel model, string code, string confirmCode);

    Task<BaseResponse<ClaimsIdentity>> IsCreatedAccount(GoogleOAuth model);

    Task SendEmail(string email, string confirmationCode);
}