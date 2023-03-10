using System.Reflection;

namespace Prokompetence.Web.PublicApi.Extensions;

public static class AppDomainExtensions
{
    public static Assembly[] GetAssembliesWithPrefixes(this AppDomain source, params string[] prefixes) =>
        source.GetAssemblies()
            .Where(assembly => prefixes.Any(prefix => assembly.FullName?.StartsWith(prefix) == true))
            .ToArray();
}