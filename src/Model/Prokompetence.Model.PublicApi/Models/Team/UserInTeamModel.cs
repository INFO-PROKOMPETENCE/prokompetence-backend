namespace Prokompetence.Model.PublicApi.Models.Team;

public sealed class UserInTeamModel
{
    public Guid StudentId { get; set; }
    public string StudentName { get; set; }
    public string StudentAcademicGroup { get; set; }
    public string StudentContacts { get; set; }
    public int? RoleId { get; set; }
    public bool IsTeamLead { get; set; }
}