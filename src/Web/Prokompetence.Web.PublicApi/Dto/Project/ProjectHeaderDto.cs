namespace Prokompetence.Web.PublicApi.Dto.Project;

public sealed class ProjectHeaderDto
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
    public int Complexity { get; set; }
}