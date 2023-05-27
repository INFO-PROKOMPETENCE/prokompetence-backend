using System.Reflection;
using LightInject.Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Prokompetence.Common.BclExtensions;
using Prokompetence.Common.Configuration;

namespace Prokompetence.Web.Admin;

internal static class Program
{
    public static void Main(string[] args)
    {
        LoadReferencesProjects(Assembly.GetExecutingAssembly());

        WebHost.CreateDefaultBuilder<Startup>(args)
            .UseLightInject()
            .Build()
            .Run();
    }

    private static void LoadReferencesProjects(Assembly assembly)
    {
        var assemblyNamesToLoad = assembly
            .GetReferencedAssemblies()
            .WithPrefixes(Constants.AssembliesPrefix);
        var assemblyNamesLoaded = new HashSet<AssemblyName>();
        while (assemblyNamesToLoad.Any())
        {
            var assemblyNamesNextToLoad = new List<AssemblyName>();
            foreach (var assemblyName in assemblyNamesToLoad.Where(assemblyName =>
                         !assemblyNamesLoaded.Contains(assemblyName)))
            {
                Assembly.Load(assemblyName);
                assemblyNamesLoaded.Add(assemblyName);
                var loadedAssembly = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Single(a => a.FullName == assemblyName.FullName);
                var referencesAssemblies =
                    loadedAssembly.GetReferencedAssemblies().WithPrefixes(Constants.AssembliesPrefix);
                assemblyNamesNextToLoad.AddRange(referencesAssemblies);
            }

            assemblyNamesToLoad = assemblyNamesNextToLoad.ToArray();
        }
    }
}