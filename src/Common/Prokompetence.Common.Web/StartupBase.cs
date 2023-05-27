using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text.Json.Serialization;
using LightInject;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prokompetence.Common.BclExtensions;
using Prokompetence.Common.Configuration;
using Prokompetence.Common.Security;
using Prokompetence.DAL;
using Prokompetence.DAL.Postgres;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Prokompetence.Common.Web;

public abstract class StartupBase
{
    private readonly IServiceProvider serviceProvider;
    private readonly IConfiguration configuration;

    protected StartupBase(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        configuration = serviceProvider.GetService<IConfiguration>() ?? throw new InvalidOperationException();
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
        services.AddSwaggerGen(ConfigureSwaggerInternal);
        services.AddHttpContextAccessor();
        services.AddCors();

        ConfigureMapster();
        ConfigureServicesAdditionally(services);
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

        var allowOrigins = configuration.GetSection("Cors:AllowOrigins").Get<string[]>() ?? Array.Empty<string>();
        app.UseCors(builder => builder
            .WithOrigins(allowOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());

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

        AfterConfigureContainer(container);
    }

    protected virtual void ConfigureServicesAdditionally(IServiceCollection services)
    {
    }

    protected virtual void ConfigureSwagger(SwaggerGenOptions options)
    {
    }

    protected virtual void ConfigureBeforeMiddlewares(IApplicationBuilder app)
    {
    }

    protected virtual void ConfigureBeforeRouting(IApplicationBuilder app)
    {
    }

    protected virtual void ConfigureBetweenRoutingAndAuthentication(IApplicationBuilder app)
    {
    }

    protected virtual void ConfigureBetweenAuthenticationAndControllers(IApplicationBuilder app)
    {
    }

    protected virtual void ConfigureAfterControllers(IApplicationBuilder app)
    {
    }

    protected virtual void AfterConfigureContainer(IServiceContainer serviceContainer)
    {
    }

    private static void ConfigureMapster()
    {
        TypeAdapterConfig.GlobalSettings.Scan(
            AppDomain.CurrentDomain.GetAssembliesWithPrefixes(Constants.AssembliesPrefix));
        TypeAdapterConfig.GlobalSettings.Compile();
    }

    private void ConfigureSwaggerInternal(SwaggerGenOptions options)
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
        var xmlFile = $"{Assembly.GetEntryAssembly()!.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);

        ConfigureSwagger(options);
    }
}