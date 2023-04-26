using Mapster;
using Prokompetence.DAL.Entities;
using Prokompetence.Model.PublicApi.Models.Project;

namespace Prokompetence.Model.PublicApi.MapsterConfigs;

public sealed class TeamInProjectConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Team, TeamInProjectModel>()
            .Map(dest => dest.TeamId, source => source.Id);
    }
}