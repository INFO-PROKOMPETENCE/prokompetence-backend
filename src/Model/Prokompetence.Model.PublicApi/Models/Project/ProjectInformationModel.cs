using Prokompetence.Model.PublicApi.Models.Team;

namespace Prokompetence.Model.PublicApi.Models.Project;

public sealed class ProjectInformationModel
{
    public string Description { get; set; }
    public string Target { get; set; }
    public string ExpectedResults { get; set; }
    public string Stack { get; set; }
    public string CuratorContacts { get; set; }
    public TeamModel[] Teams { get; set; }
}