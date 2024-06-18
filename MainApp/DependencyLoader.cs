using System.Reflection;

namespace MainApp;

public static class DependencyLoader
{
    public static void LoadDependencies(string[] dependencyPaths)
    {
        AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
        {
            string assemblyName = new AssemblyName(args.Name).Name + ".dll";
            foreach (var path in dependencyPaths)
            {
                string fullPath = Path.Combine(path, assemblyName);
                if (File.Exists(fullPath))
                {
                    return Assembly.LoadFrom(fullPath);
                }
            }
            return null;
        };
    }
}
