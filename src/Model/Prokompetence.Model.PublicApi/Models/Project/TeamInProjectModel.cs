using Prokompetence.Model.PublicApi.Models.Team;

namespace Prokompetence.Model.PublicApi.Models.Project;

public sealed class TeamInProjectModel
{
    public Guid TeamId { get; set; }
    public string Name { get; set; }
    public UserInTeamModel[] Students { get; set; }
}