using System.Reflection;

namespace Prokompetence.Common.BclExtensions;

public static class AssemblyNameExtensions
{
    public static AssemblyName[] WithPrefixes(
        this IEnumerable<AssemblyName> source,
        params string[] prefixes)
        => source.Where(
                assembly => prefixes.Any(prefix => assembly.FullName.StartsWith(prefix)))
            .ToArray();
}