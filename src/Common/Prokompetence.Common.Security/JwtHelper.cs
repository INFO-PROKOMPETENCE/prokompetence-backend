using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Prokompetence.Common.Security;

public static class JwtHelper
{
    public static SecurityKey GetSymmetricSecurityKey(string key)
        => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

    public static string GenerateRefreshToken()
    {
        var bytes = CryptographyHelper.GenerateRandomBytes(32);
        return Convert.ToBase64String(bytes);
    }
}