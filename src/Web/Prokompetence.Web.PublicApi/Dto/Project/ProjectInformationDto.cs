using Prokompetence.Web.PublicApi.Dto.Team;

namespace Prokompetence.Web.PublicApi.Dto.Project;

public sealed class ProjectInformationDto
{
    public string Description { get; set; }
    public string Target { get; set; }
    public string ExpectedResults { get; set; }
    public string Stack { get; set; }
    public string CuratorContacts { get; set; }
    public TeamDto[] Teams { get; set; }
}