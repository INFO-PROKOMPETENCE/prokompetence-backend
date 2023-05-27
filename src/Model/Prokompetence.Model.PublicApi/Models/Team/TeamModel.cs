namespace Prokompetence.Model.PublicApi.Models.Team;

public sealed class TeamModel
{
    public Guid TeamId { get; set; }
    public string Name { get; set; }
    public UserInTeamModel[] Students { get; set; }
}