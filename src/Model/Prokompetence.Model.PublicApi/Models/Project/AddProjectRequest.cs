namespace Prokompetence.Model.PublicApi.Models.Project;

public sealed class AddProjectRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Target { get; set; }
    public string ExpectedResults { get; set; }
    public string Stack { get; set; }
    public int MaxStudentsCountInTeam { get; set; }
    public int MaxTeamsCount { get; set; }
    public bool IsOpened { get; set; }
    public Guid OrganizationId { get; set; }
    public int LifeScenarioId { get; set; }
    public int KeyTechnologyId { get; set; }
    public int Complexity { get; set; }
}