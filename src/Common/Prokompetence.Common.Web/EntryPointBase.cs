using System.Reflection;
using LightInject.Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Prokompetence.Common.BclExtensions;
using Prokompetence.Common.Configuration;

namespace Prokompetence.Common.Web;

public static class EntryPointBase
{
    public static void Run<TStartup>(string[]? args = null) where TStartup : StartupBase
    {
        args ??= Array.Empty<string>();

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        LoadReferencesProjects(assemblies);

        WebHost.CreateDefaultBuilder<TStartup>(args)
            .UseLightInject()
            .Build()
            .Run();
    }

    private static void LoadReferencesProjects(IEnumerable<Assembly> assemblies)
    {
        var assemblyNamesToLoad = assemblies.SelectMany(a => a.GetReferencedAssemblies())
            .Distinct()
            .WithPrefixes(Constants.AssembliesPrefix);
        var assemblyNamesLoaded = new HashSet<AssemblyName>();
        while (assemblyNamesToLoad.Any())
        {
            var assemblyNamesNextToLoad = new List<AssemblyName>();
            foreach (var assemblyName in assemblyNamesToLoad.Where(assemblyName =>
                         !assemblyNamesLoaded.Contains(assemblyName)))
            {
                var loadedAssembly = Assembly.Load(assemblyName);
                assemblyNamesLoaded.Add(assemblyName);
                var referencesAssemblies =
                    loadedAssembly.GetReferencedAssemblies().WithPrefixes(Constants.AssembliesPrefix);
                assemblyNamesNextToLoad.AddRange(referencesAssemblies);
            }

            assemblyNamesToLoad = assemblyNamesNextToLoad.ToArray();
        }
    }
}