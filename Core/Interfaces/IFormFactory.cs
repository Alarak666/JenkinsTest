namespace FZFarm.Core.Interfaces;

public interface IFormFactory
{
    Form CreateForm(Type formType);
}