using System.Reflection;
using Autofac;
using FZFarm.Core.Models;
using FZFarm.Core.Services.Factories;

namespace FZFarm.Core.Helpers
{
    public class ContainerConfig
    {
        private readonly string _baseDirectory;
        private readonly string[] _assemblyPrefixes;
        public List<FormInfo> FormsInfo { get; private set; }

        public ContainerConfig(string baseDirectory, params string[] assemblyPrefixes)
        {
            _baseDirectory = baseDirectory;
            _assemblyPrefixes = assemblyPrefixes;
            FormsInfo = new List<FormInfo>();
        }

        public IContainer Configure()
        {
            var builder = new ContainerBuilder();

            // Регистрация фабрики форм
            builder.RegisterType<FormFactory>().AsSelf().SingleInstance();

            // Регистрация зависимостей
            RegisterDependencies(builder, _baseDirectory);

            // Создание контейнера
            return builder.Build();
        }

        private void RegisterDependencies(ContainerBuilder builder, string baseDirectory)
        {
            // Загрузка всех сборок из указанного пути
            var dllFiles = Directory.GetFiles(baseDirectory, "*.dll", SearchOption.AllDirectories);
            var loadedAssemblies = new HashSet<string>();

            foreach (var dll in dllFiles)
            {
                Console.WriteLine($"Attempting to load assembly from {dll}");
                if (File.Exists(dll) && !loadedAssemblies.Contains(dll))
                {
                    try
                    {
                        var assembly = Assembly.LoadFrom(dll);
                        Console.WriteLine($"Successfully loaded assembly from {dll}");

                        // Исключаем типы без публичных конструкторов
                        builder.RegisterAssemblyTypes(assembly)
                            .Where(t => t.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Any())
                            .AsSelf()
                            .AsImplementedInterfaces();

                        loadedAssemblies.Add(dll);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to load assembly from {dll}: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"File does not exist or already loaded: {dll}");
                }
            }

            //// Автоматическая регистрация всех доступных библиотек
            //RegisterProjectAssemblies(builder);
        }

        //private void RegisterProjectAssemblies(ContainerBuilder builder)
        //{
        //    // Получение всех загруженных сборок в текущем AppDomain
        //    var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        //    foreach (var assembly in assemblies)
        //    {
        //        // Фильтруем только ваши собственные сборки
        //        if (_assemblyPrefixes.Any(prefix => assembly.FullName.StartsWith(prefix)))
        //        {
        //            builder.RegisterAssemblyTypes(assembly)
        //                   .Where(t => t.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Any())
        //                   .AsSelf()
        //                   .AsImplementedInterfaces();
        //        }
        //    }
        //}
    }
}
