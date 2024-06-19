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
            // ���� � �����, ���������� ��� ����������� ����������� � ������������
            string basePath = @"D:\Git\AdminPortal\AdminPortal\bin\Debug\net8.0-windows";

            // �������� ������������
            DependencyLoader.LoadDependencies(new string[] { basePath });

            // �������� ������������
            LoadAllConfigurations(basePath);

            // ���� � �������� ������
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
                    // ����� �� ������ ���������� ����������� ������������
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