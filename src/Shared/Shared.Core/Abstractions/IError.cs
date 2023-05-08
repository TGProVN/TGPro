namespace Shared.Core.Abstractions;

public interface IError
{
    string? StackTrace { get; set; }
}
