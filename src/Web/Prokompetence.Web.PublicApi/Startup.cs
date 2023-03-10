using LightInject;
using Prokompetence.Web.PublicApi.Extensions;

namespace Prokompetence.Web.PublicApi;

public sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }

    public void ConfigureContainer(IServiceContainer container)
    {
        var assemblies = AppDomain.CurrentDomain
            .GetAssembliesWithPrefixes(Constants.AssembliesPrefix);
        foreach (var assembly in assemblies)
        {
            container.RegisterAssembly(
                assembly,
                () => new PerRequestLifeTime(),
                (serviceType, _) => serviceType.IsInterface
            );
        }
    }
}