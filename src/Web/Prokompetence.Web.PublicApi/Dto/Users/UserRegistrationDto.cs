namespace Prokompetence.Web.PublicApi.Dto.Users;

/// <summary>
/// Dto for registration
/// </summary>
/// <param name="Login"></param>
/// <param name="Password"></param>
public sealed record UserRegistrationDto(
    string Login,
    string Password
);