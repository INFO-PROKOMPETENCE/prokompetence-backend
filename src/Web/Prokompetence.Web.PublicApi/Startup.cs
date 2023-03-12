using System.Reflection;
using System.Text.Json.Serialization;
using LightInject;
using Mapster;
using Microsoft.OpenApi.Models;
using Prokompetence.Common.BclExtensions;
using Prokompetence.Common.Configuration;

namespace Prokompetence.Web.PublicApi;

public sealed class Startup
{
    private readonly IWebHostEnvironment environment;

    public Startup(IWebHostEnvironment environment)
    {
        this.environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        if (environment.IsDevelopment())
        {
            services.AddSwaggerGen(configure =>
            {
                configure.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Prokompetence",
                    Description = "Public API for Prokompetence"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                configure.IncludeXmlComments(xmlPath);
            });
        }

        ConfigureMapster();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = "swagger";
            });
        }

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

    public static void ConfigureMapster()
    {
        TypeAdapterConfig.GlobalSettings.Scan(
            AppDomain.CurrentDomain.GetAssembliesWithPrefixes(Constants.AssembliesPrefix));
        TypeAdapterConfig.GlobalSettings.Compile();
    }
}