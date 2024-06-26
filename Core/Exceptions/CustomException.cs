using FZFarm.Core.Constants.DefaultValues;
using FZFarm.Core.Constants.Enums;

namespace FZFarm.Core.Exceptions;

public class CustomException : Exception
{
    public const string MessageError = CoreDefaultStrings.CustomExceptionText;

    public CustomException(string message)
        : base(string.Format(MessageError, message))
    {
    }

    public ErrorType ErrorType = ErrorType.Customs;
}