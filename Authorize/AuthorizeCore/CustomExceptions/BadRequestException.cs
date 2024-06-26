using FZFarm.AuthorizeCore.Constants.DefaultValues;
using FZFarm.AuthorizeCore.Constants.Enums;

namespace FZFarm.AuthorizeCore.CustomExceptions;

public class BadRequestException : Exception
{
    public const string MessageError = CoreDefaultStrings.BadRequestError;

    public BadRequestException()
        : base(string.Format(MessageError))
    {
    }
    public ErrorType ErrorType = ErrorType.BadRequest;
}