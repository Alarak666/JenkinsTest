using FZFarm.Core.Constants.DefaultValues;
using FZFarm.Core.Constants.Enums;

namespace FZFarm.Core.Exceptions;

public class BadRequestException : Exception
{
    public const string MessageError = CoreDefaultStrings.BadRequestError;

    public BadRequestException()
        : base(string.Format(MessageError))
    {
    }
    public ErrorType ErrorType = ErrorType.BadRequest;
}