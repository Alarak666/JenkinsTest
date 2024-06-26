using FZFarm.AuthorizeCore.Constants.Enums;

namespace FZFarm.AuthorizeCore.Model;

public class ErrorInfo
{
    public ErrorType Type { get; set; }
    public string Message { get; set; }
}