using Shared.Core.Abstractions;

namespace Shared.Core.Wrapper.Errors;

public class SystemError : IError
{
    public string? FileName { get; set; }
    public string? Method { get; set; }
    public string? LineNumber { get; set; }
    public string? StackTrace { get; set; }
}
