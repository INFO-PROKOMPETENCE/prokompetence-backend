using System.ComponentModel.DataAnnotations;

namespace Prokompetence.Web.PublicApi.Dto.Users;

/// <summary>
/// Dto for registration
/// </summary>
/// <param name="Login"></param>
/// <param name="Password"></param>
/// <param name="Name"></param>
public sealed record UserRegistrationDto(
    [Required] string Login,
    [Required] string Password,
    [Required] string Name
);