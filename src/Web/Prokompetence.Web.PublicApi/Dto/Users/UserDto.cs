namespace Prokompetence.Web.PublicApi.Dto.Users;

/// <summary>
/// Base user information
/// </summary>
public class UserDto
{
    public string Name { get; set; }
    public string? Contacts { get; set; }
    public string? AcademicGroup { get; set; }
}