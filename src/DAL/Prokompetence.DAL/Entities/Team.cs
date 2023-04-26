namespace Prokompetence.DAL.Entities;

public sealed class Team
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Guid TeamLeadId { get; set; }
    public User TeamLead { get; set; }

    public ISet<TeamProjectRecord> Records { get; set; }
    public ISet<StudentInTeam> Students { get; set; }
}