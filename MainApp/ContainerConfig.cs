using System.Reflection;
using Autofac;

namespace MainApp;

public class ContainerConfig
{
    private readonly string _assemblyPath;
    public List<FormInfo> FormsInfo { get; private set; }

    public ContainerConfig(string assemblyPath)
    {
        _assemblyPath = assemblyPath;
        FormsInfo = new List<FormInfo>();
    }

    public IContainer Configure()
    {
        var builder = new ContainerBuilder();

        // Регистрация всех форм в сборке
        Assembly assembly = Assembly.LoadFrom(_assemblyPath);
        builder.RegisterAssemblyTypes(assembly)
            .Where(t => t.IsSubclassOf(typeof(Form)))
            .AsSelf();

        // Собираем информацию о формах
        foreach (var type in assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Form))))
        {
            var parameters = string.Join(", ", type.GetConstructors()
                .SelectMany(c => c.GetParameters())
                .Select(p => $"{p.ParameterType.Name} {p.Name}"));

            FormsInfo.Add(new FormInfo
            {
                Name = type.Name,
                Parameters = parameters
            });
        }

        // Регистрация фабрики
        builder.RegisterType<FormFactory>().AsSelf();

        // Создание контейнера
        return builder.Build();
    }
}