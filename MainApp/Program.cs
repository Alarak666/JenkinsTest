using Autofac;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows.Forms;

namespace MainApp
{
    public static class Program
    {
        public static IContainer Container { get; private set; }

        [STAThread]
        static void Main()
        {
            // Путь к папке, содержащей все необходимые зависимости и конфигурации
            string basePath = @"D:\Git\AdminPortal\AdminPortal\bin\Debug\net8.0-windows";

            // Загрузка зависимостей
            DependencyLoader.LoadDependencies(new string[] { basePath });

            // Загрузка конфигураций
            LoadAllConfigurations(basePath);

            // Путь к основной сборке
            string assemblyPath = Path.Combine(basePath, "AdminPortal.dll");
            var containerConfig = new ContainerConfig(assemblyPath);
            Container = containerConfig.Configure();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void LoadAllConfigurations(string basePath)
        {
            var configFiles = Directory.GetFiles(basePath, "*.json");
            foreach (var configFile in configFiles)
            {
                try
                {
                    var configuration = ConfigurationHelper.LoadConfiguration<JObject>(configFile);
                    // Здесь вы можете обработать загруженную конфигурацию
                    Console.WriteLine($"Loaded configuration from {configFile}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading configuration from {configFile}: {ex.Message}");
                }
            }
        }
    }
}