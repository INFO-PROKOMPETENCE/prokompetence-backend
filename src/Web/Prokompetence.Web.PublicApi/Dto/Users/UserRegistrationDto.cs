namespace Prokompetence.Web.PublicApi.Dto.Users;

public sealed record UserRegistrationDto(
    string Login,
    string Password
);