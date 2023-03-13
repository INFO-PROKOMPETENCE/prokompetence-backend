namespace Prokompetence.Web.PublicApi.Dto.Users;

public sealed record UserLoginDto(
    string Login,
    string Password
);