using FZFarm.AuthorizeCore.Constants.DefaultValues;
using FZFarm.AuthorizeCore.Constants.Enums;

namespace FZFarm.AuthorizeCore.CustomExceptions
{
    public class LivePasswordFailedException : Exception
    {
        public const string MessageError = CoreDefaultStrings.LivePasswordBad;

        public LivePasswordFailedException()
            : base(string.Format(MessageError))
        {
        }

        public ErrorType ErrorType = ErrorType.LivePasswordLeft;
    }
}
