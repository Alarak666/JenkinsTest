using System.Reflection;
using FZFarm.Core.Helpers;
using Newtonsoft.Json.Linq;

namespace FZFarm.Core.Services.Loaders
{
    public static class DependencyLoader
    {
        private static List<string> loadedAssemblies = new List<string>();
       
        public static void LoadDependencies(string[] dependencyPaths)
        {
            // Загрузка всех DLL из указанных директорий
            foreach (var path in dependencyPaths)
            {
                LoadAssembliesFromPath(path);
            }

            // Установка обработчика для загрузки зависимостей при необходимости
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string assemblyName = new AssemblyName(args.Name).Name + ".dll";
                foreach (var path in dependencyPaths)
                {
                    string fullPath = Path.Combine(path, assemblyName);
                    if (File.Exists(fullPath))
                    {
                        Console.WriteLine($"Loading assembly from {fullPath}");
                        try
                        {
                            var assembly = Assembly.LoadFrom(fullPath);
                            loadedAssemblies.Add(fullPath);
                            return assembly;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to load assembly from {fullPath}: {ex.Message}");
                        }
                    }
                }
                Console.WriteLine($"Failed to load assembly: {args.Name}");
                return null;
            };

            LoadSupportFiles(dependencyPaths);
        }

        private static void LoadAssembliesFromPath(string path)
        {
            var dllFiles = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
            foreach (var dll in dllFiles)
            {
                try
                {
                    Console.WriteLine($"Loading assembly from {dll}");
                    var assembly = Assembly.LoadFrom(dll);
                    loadedAssemblies.Add(dll);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load assembly from {dll}: {ex.Message}");
                }
            }
        }

        private static void LoadSupportFiles(string[] dependencyPaths)
        {
            foreach (var path in dependencyPaths)
            {
                var supportFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                                            .Where(file => file.EndsWith(".json") || file.EndsWith(".xml") || file.EndsWith(".config"));

                foreach (var supportFile in supportFiles)
                {
                    try
                    {
                        // Обработка JSON файлов
                        if (supportFile.EndsWith(".json"))
                        {
                            var configuration = ConfigurationHelper.LoadConfiguration<JObject>(supportFile);
                            // Здесь вы можете обработать загруженную конфигурацию
                            Console.WriteLine($"Loaded JSON configuration from {supportFile}");
                        }
                        // Добавьте обработку других типов файлов (XML, .config) по мере необходимости
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading support file {supportFile}: {ex.Message}");
                    }
                }
            }
        }

        public static void PrintLoadedAssemblies()
        {
            Console.WriteLine("Loaded assemblies:");
            foreach (var assembly in loadedAssemblies)
            {
                Console.WriteLine(assembly);
            }
        }
    }
}
