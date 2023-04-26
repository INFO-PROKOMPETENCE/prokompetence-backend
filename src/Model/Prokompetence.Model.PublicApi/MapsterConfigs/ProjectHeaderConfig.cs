using Mapster;
using Prokompetence.DAL.Entities;
using Prokompetence.Model.PublicApi.Models.Project;

namespace Prokompetence.Model.PublicApi.MapsterConfigs;

public sealed class ProjectHeaderConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Project, ProjectHeaderModel>()
            .Map(destination => destination.RecordedTeamsCount, source => source.Records.Count);
    }
}