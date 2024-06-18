using Autofac;
using MainApp;

public static class Program
{
    public static IContainer Container { get; private set; }

    [STAThread]
    static void Main()
    {
        string[] dependencyPaths = { @"D:\Git\AdminPortal\AdminPortal\bin\Debug\net8.0-windows" };

        // Загрузка зависимостей
        DependencyLoader.LoadDependencies(dependencyPaths);
        string assemblyPath = @"D:\Git\AdminPortal\AdminPortal\bin\Debug\net8.0-windows\AdminPortal.dll";
        var containerConfig = new ContainerConfig(assemblyPath);
        Container = containerConfig.Configure();

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());
    }
}
