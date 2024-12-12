using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Security.Claims;
using AutoMapper;
using GMarket.DAL.Interfaces;
using GMarket.Domain.Entities.IdentityCustomer;
using GMarket.Domain.Helpers;
using GMarket.Domain.Response;
using GMarket.Domain.Types;
using GMarket.Domain.ViewModels;
using GMarket.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace GMarket.Service.Services;

public class AccountService : IAccountService
{
    private readonly IBaseStorage<UserSecurity> _userSecurityStorage;
    private readonly IBaseStorage<CustomerContext> _customerContext;
    
    private IMapper _mapper { get; set; }
    
    private IConfiguration _configuration { get; set; }

    private MapperConfiguration _mapperConfig = new MapperConfiguration(p =>
    {
        p.AddProfile<AppMappingProfile>();
    });

    public AccountService(
        IBaseStorage<UserSecurity> userSecurityStorage,
        IBaseStorage<CustomerContext> customerContext,
        IConfiguration configuration)
    {
        _userSecurityStorage = userSecurityStorage;
        _customerContext = customerContext;
        _configuration = configuration;
        _mapper = _mapperConfig.CreateMapper();
    }
    
    public async Task<BaseResponse<string>> Register(RegisterViewModel modelIn)
    {
        try
        {
            Random random = new Random();
            string confirmCode = $"{random.Next(10)}{random.Next(10)}{random.Next(10)}{random.Next(10)}";
            
            var userDb = await _userSecurityStorage.GetAllAsync().FirstOrDefaultAsync(x => x.Email == modelIn.Email);

            if (userDb != null)
            {
                return new BaseResponse<string>()
                {
                    Description = "Пользователь с такой почтой существует"
                };
            }

            await SendEmail(modelIn.Email, confirmCode);

            return new BaseResponse<string>()
            {
                Data = confirmCode,
                Description = "Письмо отправлено",
                StatusCode = StatusCode.Ok,
            };
        }
        catch (ValidationException ex)
        {
            return new BaseResponse<string>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.BadRequest,
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<string>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel modelIn)
    {
        try
        {
            var userDb = await _userSecurityStorage.GetAllAsync().FirstOrDefaultAsync(x => x.Email == modelIn.Email);
            var userContext = await _customerContext.GetAllAsync().FirstOrDefaultAsync(x => x.UserSecurity == userDb);

            if (userContext == null)
            {
                userContext = new CustomerContext(
                    userDb.Email,
                    "First Name Last Name",
                    userDb
                    );
                
                userContext = await _customerContext.AddAsync(userContext);
            }
            if (userDb == null)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Пользователь не найден",
                    StatusCode = StatusCode.BadRequest
                };
            }
            else if (userDb.Password != HashPasswordHelper.HashPassword(modelIn.Password))
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Неверный пароль или почта",
                    StatusCode = StatusCode.BadRequest
                };
            }
            else
            {
                var result = AuthenticateUserHelper.Authenticate(userDb, userContext);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Успешная аутентификация",
                    StatusCode = StatusCode.Ok
                };
            }
        }
        catch (Exception ex)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task SendEmail(string email, string confirmationCode)
    {
        var message = new MimeMessage();
        
        message.From.Add(new MailboxAddress("Администратор Сайта", "Gmarket@bk.ru"));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = "Добро пожаловать";
        message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = "<html>" + "<head>" +"<style>" +
                   "body { font-family: Arial, sans-serif; background-color: #f2f2f2; }" +
                   ".container { max-width: 600px; margin: 0 auto; padding: 20px; background-color: #fff; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1); }" +
                   ".header { text-align: center; margin-bottom: 20px; }" +
                   ".message { font-size: 16px; line-height: 1.6; }"
                   +
                   ".container-code { background-color: #f0ffff; padding: 5px; border-radius: 5px; font-weight: bold; }"+
            ".code {text-align: center; }" +
            "</style>" +
            "</head>" +
            "<body>" +
            "<div class='container'>" +
            "<div class='header'><h1>Добро пожаловать на сайт Садовничества!</h1></div>" +
            "<div class='message'>" +
            "<p>Пожалуйста, введите данный код на сайте, чтобы подтвердить ваш email и завершить регистрацию:</p>" +
            "<div class='container-code'><p class='code'>" + confirmationCode + "</p></div>" +
            "</div>" +
            "</div>" +"</body>" +"</html>"
        };

        using(var client = new MailKit.Net.Smtp.SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync("vymasfyaya@gmail.com",
                _configuration.GetConnectionString("Google"));

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

    public async Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(RegisterViewModel modelIn, string code, string confirmCode)
    {
        try
        {
            if (code != confirmCode)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "Confirm code is invalid",
                    StatusCode = StatusCode.BadRequest,
                };
            }
            
            modelIn.Password = HashPasswordHelper.HashPassword(modelIn.Password);
            var model = _mapper.Map<UserSecurity>(modelIn);
            try
            {
                var userDb = await _userSecurityStorage.GetAllAsync().FirstOrDefaultAsync(x => x.Email == modelIn.Email);

                if (userDb != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользователь с такой почтой существует"
                    };
                }

                var securityModel = await _userSecurityStorage.AddAsync(model);
                var customerModel = await _customerContext.AddAsync(new CustomerContext(
                    model.Email,
                    "First Name Last Name",
                    model));

                var result = AuthenticateUserHelper.Authenticate(securityModel, customerModel);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Пользователь зарегистрирован",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                try{await _userSecurityStorage.DeleteAsync(model);}
                catch(Exception exx){}
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
                
                
        }
        catch (Exception ex)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError,
            };
        }
    }


    public async Task<BaseResponse<ClaimsIdentity>> IsCreatedAccount(GoogleOAuth modelIn)
    {
        UserSecurity findUser;
        try
        {
            findUser = await _userSecurityStorage.GetAllAsync().FirstOrDefaultAsync(x => x.Email == modelIn.Email);
            if (findUser == null)
            {
                try
                {
                    UserSecurity user = new UserSecurity(
                        "google",
                        modelIn.Email);

                    var securityModel = await _userSecurityStorage.AddAsync(user);

                    CustomerContext customerModel = await _customerContext.AddAsync(new CustomerContext(
                        modelIn.Login,
                        modelIn.Name,
                        securityModel)
                    {
                        S3Image = modelIn.Image ?? "/img/user.svg",
                    });
                    
                    var result = AuthenticateUserHelper.Authenticate(securityModel, customerModel);

                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Data = result,
                        Description = "Пользователь зарегистрирован",
                        StatusCode = StatusCode.Ok
                    };
                }
                catch (Exception ex2)
                {   
                    var findUser_ = await _userSecurityStorage.GetAllAsync().FirstOrDefaultAsync(x => x.Email == modelIn.Email);
                    if (findUser_ != null)
                    {
                        await _userSecurityStorage.DeleteAsync(findUser_);
                    }
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = ex2.Message,
                        StatusCode = StatusCode.InternalServerError
                    };
                }
            }

            
            var resultLogin = AuthenticateUserHelper.Authenticate(findUser, await _customerContext.GetAllAsync().FirstOrDefaultAsync(x=>x.UserSecurityId == findUser.Id));
            return new BaseResponse<ClaimsIdentity>(){
                Data = resultLogin,
                Description = "Already",
                StatusCode = StatusCode.Ok,
            };
        }
        catch(Exception ex)
        {
            return new BaseResponse<ClaimsIdentity>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}