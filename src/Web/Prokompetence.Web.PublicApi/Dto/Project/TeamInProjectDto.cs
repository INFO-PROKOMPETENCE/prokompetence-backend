using Prokompetence.Web.PublicApi.Dto.Team;

namespace Prokompetence.Web.PublicApi.Dto.Project;

public sealed class TeamInProjectDto
{
    public Guid TeamId { get; set; }
    public string Name { get; set; }
    public UserInTeamDto[] Students { get; set; }
}