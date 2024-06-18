using Autofac;
using MainApp;
using System.Reflection;

public class ContainerConfig
{
    private readonly string _assemblyPath;

    public ContainerConfig(string assemblyPath)
    {
        _assemblyPath = assemblyPath;
    }

    public IContainer Configure()
    {
        var builder = new ContainerBuilder();

        // Регистрация всех форм в сборке
        Assembly assembly = Assembly.LoadFrom(_assemblyPath);
        builder.RegisterAssemblyTypes(assembly)
               .Where(t => t.IsSubclassOf(typeof(Form)))
               .AsSelf();

        // Регистрация фабрики
        builder.RegisterType<FormFactory>().AsSelf();

        // Создание контейнера
        return builder.Build();
    }
}
