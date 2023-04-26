using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prokompetence.DAL.Entities;

namespace Prokompetence.DAL.Configs;

public sealed class LifeScenarioConfig : IEntityTypeConfiguration<LifeScenario>
{
    public void Configure(EntityTypeBuilder<LifeScenario> builder)
    {
        builder.Property(e => e.Id)
            .ValueGeneratedNever();
    }
}