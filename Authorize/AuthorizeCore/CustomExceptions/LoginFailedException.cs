using FZFarm.AuthorizeCore.Constants.DefaultValues;
using FZFarm.AuthorizeCore.Constants.Enums;

namespace FZFarm.AuthorizeCore.CustomExceptions
{
    public class LoginFailedException : Exception
    {
        public const string MessageError = CoreDefaultStrings.LoginFailedError;

        public LoginFailedException()
            : base(string.Format(MessageError))
        {
        }

        public ErrorType ErrorType = ErrorType.LoginFailed;
    }
}
