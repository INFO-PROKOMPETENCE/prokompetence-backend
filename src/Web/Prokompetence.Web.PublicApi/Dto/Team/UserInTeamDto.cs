namespace Prokompetence.Web.PublicApi.Dto.Team;

public sealed class UserInTeamDto
{
    public Guid StudentId { get; set; }
    public string StudentName { get; set; }
    public int? RoleId { get; set; }
    public bool IsTeamLead { get; set; }
}