using Shared.Core.Abstractions;

namespace Shared.Core.Wrapper.Errors;

public class ValidationError : IError
{
    public string? Code { get; set; }
    public string? Message { get; set; }
    public string? PropertyName { get; set; }
    public string? StackTrace { get; set; }
}
