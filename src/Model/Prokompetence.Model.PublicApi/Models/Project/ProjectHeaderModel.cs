namespace Prokompetence.Model.PublicApi.Models.Project;

public class ProjectHeaderModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public int MaxStudentsCountInTeam { get; set; }
    public int MaxTeamsCount { get; set; }
    public int RecordedTeamsCount { get; set; }
    public string OrganizationName { get; set; }
    public string CuratorName { get; set; }
    public int LifeScenarioId { get; set; }
    public int KeyTechnologyId { get; set; }
}