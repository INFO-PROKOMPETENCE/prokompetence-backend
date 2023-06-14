namespace Prokompetence.Web.PublicApi.Dto.Team;

public sealed class TeamDto
{
    public Guid TeamId { get; set; }
    public string Name { get; set; }
    public UserInTeamDto[] Students { get; set; }
    public Guid? ProjectId { get; set; }
}