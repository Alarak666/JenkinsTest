using FZFarm.AuthorizeCore.Constants.Enums;

namespace FZFarm.AuthorizeCore.CustomExceptions;

public class CustomException : Exception
{

    public CustomException(string messageError)
        : base(string.Format(messageError))
    {
    }

    public ErrorType ErrorType = ErrorType.Customer;
}