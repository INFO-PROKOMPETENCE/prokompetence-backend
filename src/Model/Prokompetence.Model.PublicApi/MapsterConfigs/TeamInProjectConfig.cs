using Mapster;
using Prokompetence.DAL.Entities;
using Prokompetence.Model.PublicApi.Models.Project;
using Prokompetence.Model.PublicApi.Models.Team;

namespace Prokompetence.Model.PublicApi.MapsterConfigs;

public sealed class TeamInProjectConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Team, TeamModel>()
            .Map(dest => dest.TeamId, source => source.Id);
    }
}