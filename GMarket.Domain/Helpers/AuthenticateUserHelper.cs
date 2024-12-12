using System.Security.Claims;
using GMarket.Domain.Entities.IdentityCustomer;

namespace GMarket.Domain.Helpers;

public static class AuthenticateUserHelper
{
    public static ClaimsIdentity Authenticate(UserSecurity userSecurity, CustomerContext userContext)
    {
        var claimsIdentity = new List<Claim>
        {
            new Claim(ClaimTypes.Email, userSecurity.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, userSecurity.Role.ToString()),
            new Claim("AvatarPath", userContext.S3Image),
            new Claim(ClaimTypes.Name, userContext.FullName),
            new Claim("UserName", userContext.UserName)
        };
        return new ClaimsIdentity(claimsIdentity, "ApplicationCookie",
            ClaimTypes.Email, ClaimsIdentity.DefaultRoleClaimType);
    }
}