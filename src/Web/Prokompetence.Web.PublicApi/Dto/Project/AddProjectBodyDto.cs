namespace Prokompetence.Web.PublicApi.Dto.Project;

public sealed record AddProjectBodyDto(
    string Name,
    string Description,
    string FinalProject,
    string Stack,
    int MaxStudentsCountInTeam,
    int MaxTeamsCount,
    bool IsOpened,
    Guid OrganizationId,
    int LifeScenarioId,
    int KeyTechnologyId,
    int Complexity
);