using Mapster;
using Prokompetence.DAL.Entities;
using Prokompetence.Model.PublicApi.Models.Project;

namespace Prokompetence.Model.PublicApi.MapsterConfigs;

public sealed class ProjectInformationConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Project, ProjectInformationModel>()
            .Map(dest => dest.Teams, source => source.Records.Select(r => r.Team));
    }
}