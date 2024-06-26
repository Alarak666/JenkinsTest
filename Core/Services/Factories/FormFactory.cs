using Autofac;
using FZFarm.Core.Constants.DefaultValues;
using FZFarm.Core.Interfaces;

namespace FZFarm.Core.Services.Factories
{
    public class FormFactory : IFormFactory
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
            var constructor = constructors.MaxBy(c => c.GetParameters().Length);

            if (constructor == null)
                throw new InvalidOperationException("No constructor found for type " + formType.Name);

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
            switch (parameterName)
            {
                case "userName":
                    return CoreDefaultStrings.UserName;
                default:
                    return Activator.CreateInstance(parameterType);
            }
        }

    }
}
