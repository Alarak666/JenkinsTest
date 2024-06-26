
using FZFarm.Core.Constants.Enums;

namespace FZFarm.Core.Models.Exceptions;

public class ErrorInfo
{
    public ErrorType Type { get; set; }
    public string Message { get; set; }
}