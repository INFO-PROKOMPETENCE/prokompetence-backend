using Microsoft.AspNetCore;

namespace Prokompetence.Web.PublicApi;

internal static class Program
{
    public static void Main(string[] args)
    {
        WebHost.CreateDefaultBuilder<Startup>(args)
            .Build()
            .Run();
    }
}