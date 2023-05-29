using Prokompetence.Common.Web;

namespace Prokompetence.Web.PublicApi;

internal static class Program
{
    public static void Main(string[] args)
    {
        EntryPointBase.Run<Startup>(args);
    }
}