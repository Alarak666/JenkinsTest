using Autofac;

namespace MainApp;

public class FormFactory
{
    private readonly ILifetimeScope _scope;

    public FormFactory(ILifetimeScope scope)
    {
        _scope = scope;
    }

    public Form CreateForm(Type formType)
    {
        var constructors = formType.GetConstructors();

        // Найти конструктор с максимальным количеством параметров
        var constructor = constructors.OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();

        if (constructor == null)
        {
            throw new InvalidOperationException("No constructor found for type " + formType.Name);
        }

        var parameters = constructor.GetParameters();
        var namedParameters = new List<NamedParameter>();

        foreach (var parameter in parameters)
        {
            // Определите значения параметров на основе имени или типа параметра
            object value = GetParameterValue(parameter.Name, parameter.ParameterType);
            namedParameters.Add(new NamedParameter(parameter.Name, value));
        }

        return _scope.Resolve(formType, namedParameters.ToArray()) as Form;
    }

    private object GetParameterValue(string parameterName, Type parameterType)
    {
        // Определите значения параметров на основе имени или типа параметра
        if (parameterType == typeof(string))
        {
            return "DefaultValue"; // Пример значения для строкового параметра
        }
        else if (parameterType == typeof(int))
        {
            return 42; // Пример значения для целочисленного параметра
        }
        // Добавьте другие типы по мере необходимости

        return Activator.CreateInstance(parameterType); // Создание значения по умолчанию для остальных типов
    }
}