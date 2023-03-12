using System.Reflection;

namespace Prokompetence.Common.BclExtensions;

public static class AppDomainExtensions
{
    public static Assembly[] GetAssembliesWithPrefixes(this AppDomain source, params string[] prefixes) =>
        source.GetAssemblies()
            .Where(assembly => prefixes.Any(prefix => assembly.FullName?.StartsWith(prefix) == true))
            .ToArray();
}