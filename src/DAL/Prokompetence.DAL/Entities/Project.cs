namespace Prokompetence.DAL.Entities;

public sealed class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Target { get; set; }
    public string ExpectedResults { get; set; }
    public string Stack { get; set; }
    public int MaxStudentsCountInTeam { get; set; }
    public int MaxTeamsCount { get; set; }
    public bool IsOpened { get; set; }

    public Guid OrganizationId { get; set; }
    public Organization Organization { get; set; }

    public Guid CuratorId { get; set; }
    public User Curator { get; set; }

    public int LifeScenarioId { get; set; }
    public LifeScenario LifeScenario { get; set; }

    public int KeyTechnologyId { get; set; }
    public KeyTechnology KeyTechnology { get; set; }

    public ISet<TeamProjectRecord> Records { get; set; }
    public int Complexity { get; set; }
}