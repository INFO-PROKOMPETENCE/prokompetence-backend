﻿namespace Prokompetence.Web.PublicApi.Dto.Project;

public sealed record AddProjectBodyDto(
    string Name,
    string Description,
    string Target,
    string ExpectedResults,
    string Stack,
    int MaxStudentsCountInTeam,
    int MaxTeamsCount,
    bool IsOpened,
    Guid OrganizationId,
    int LifeScenarioId,
    int KeyTechnologyId,
    int Complexity
);