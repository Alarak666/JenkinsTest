using Autofac;
using Newtonsoft.Json.Linq;

namespace MainApp;
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

        // Печать списка загруженных сборок
        DependencyLoader.PrintLoadedAssemblies();

        // Путь к основной сборке
        string assemblyPath = Path.Combine(basePath, "AdminPortal.dll");
        var containerConfig = new ContainerConfig(assemblyPath);
        Container = containerConfig.Configure();

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());
    }
}
