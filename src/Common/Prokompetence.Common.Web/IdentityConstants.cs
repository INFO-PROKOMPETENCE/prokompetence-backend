using System.Security.Claims;

namespace Prokompetence.Common.Web;

public static class IdentityConstants
{
    public const string Id = "id";
    public const string Login = ClaimTypes.Name;
    public const string Role = ClaimTypes.Role;
}