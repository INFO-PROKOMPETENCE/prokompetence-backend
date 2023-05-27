using StartupBase = Prokompetence.Common.Web.StartupBase;

namespace Prokompetence.Web.PublicApi;

public sealed class Startup : StartupBase
{
    public Startup(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}