using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text.Json.Serialization;
using LightInject;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prokompetence.Common.BclExtensions;
using Prokompetence.Common.Configuration;
using Prokompetence.Common.Security;
using Prokompetence.DAL.EFCore;
using Prokompetence.DAL.SqlServer;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Prokompetence.Web.PublicApi;

public sealed class Startup
{
    private readonly IWebHostEnvironment environment;
    private readonly IConfiguration configuration;

    public Startup(IWebHostEnvironment environment, IConfiguration configuration)
    {
        this.environment = environment;
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var authenticationOptions =
                    configuration.GetSection("Authentication").Get<AuthenticationOptions>()
                    ?? throw new InvalidOperationException();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authenticationOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authenticationOptions.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = JwtHelper.GetSymmetricSecurityKey(authenticationOptions.Key),
                    ValidateIssuerSigningKey = true
                };
            });
        services.AddAuthorization();
        if (environment.IsDevelopment())
        {
            services.AddSwaggerGen(ConfigureSwagger);
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

        app.UseAuthentication();
        app.UseAuthorization();

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

        container.Register<ProkompetenceDbContext, SqlServerProkompetenceDbContext>(new PerRequestLifeTime());
        container.Register<ConnectionStrings>(_ =>
                configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>()
                ?? throw new InvalidOperationException(),
            new PerContainerLifetime());
        container.Register<AuthenticationOptions>(_ =>
            configuration.GetSection("Authentication").Get<AuthenticationOptions>()
            ?? throw new InvalidOperationException());
        using (var scope = container.BeginScope())
        {
            var dbContext = scope.GetInstance<ProkompetenceDbContext>();
            dbContext.Database.Migrate();
        }

        container.Register<SecurityTokenHandler, JwtSecurityTokenHandler>(new PerRequestLifeTime());
    }

    public static void ConfigureMapster()
    {
        TypeAdapterConfig.GlobalSettings.Scan(
            AppDomain.CurrentDomain.GetAssembliesWithPrefixes(Constants.AssembliesPrefix));
        TypeAdapterConfig.GlobalSettings.Compile();
    }

    private static void ConfigureSwagger(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Prokompetence",
            Description = "Public API for Prokompetence"
        });
        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Description = "Put access token here",

            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };

        options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { jwtSecurityScheme, Array.Empty<string>() }
        });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }
}