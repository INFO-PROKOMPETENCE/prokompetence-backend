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
using Prokompetence.DAL;
using Prokompetence.DAL.Postgres;
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
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = JwtHelper.GetSymmetricSecurityKey(authenticationOptions.Key),
                    ValidateIssuerSigningKey = true
                };
            });
        services.ConfigureApplicationCookie(configure =>
        {
            configure.Cookie.HttpOnly = true;
            configure.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            configure.Cookie.SameSite = SameSiteMode.None;
        });
        services.AddAuthorization();
        services.AddSwaggerGen(ConfigureSwagger);
        services.AddHttpContextAccessor();
        services.AddCors();

        ConfigureMapster();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseHttpsRedirection();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = "swagger";
        });

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        var allowOrigins = configuration.GetSection("Cors:AllowOrigins").Get<string[]>() ?? Array.Empty<string>();
        app.UseCors(builder => builder
            .WithOrigins(allowOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());

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

        container.Register<IProkompetenceDbContext, PostgresProkompetenceDbContext>(new PerScopeLifetime());
        container.Register<IUnitOfWork>(factory => factory.GetInstance<IProkompetenceDbContext>(),
            new PerScopeLifetime());
        container.Register<IConfiguration>(_ => configuration, new PerContainerLifetime());
        var settingsTypes =
            assemblies.SelectMany(a => a.GetTypes().Where(t => t.GetCustomAttribute<SettingsAttribute>() != null));
        foreach (var settingsType in settingsTypes)
        {
            container.Register(settingsType,
                factory => factory.GetInstance<IConfiguration>()
                    .GetSection(settingsType.GetCustomAttribute<SettingsAttribute>()!.Scope ?? settingsType.Name)
                    .Get(settingsType), new PerContainerLifetime());
        }

        using (var scope = container.BeginScope())
        {
            var dbContext = (ProkompetenceDbContext)scope.GetInstance<IProkompetenceDbContext>();
            dbContext.Database.Migrate();
        }

        container.Register<JwtSecurityTokenHandler>(new PerRequestLifeTime());
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