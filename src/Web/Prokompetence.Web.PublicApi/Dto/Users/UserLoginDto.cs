namespace Prokompetence.Web.PublicApi.Dto.Users;

/// <summary>
/// Dto for login by password
/// </summary>
/// <param name="Login"></param>
/// <param name="Password"></param>
public sealed record UserLoginDto(
    string Login,
    string Password
);