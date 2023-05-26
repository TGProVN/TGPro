namespace Shared.Core.Abstractions;

public interface IResult
{
    bool Succeeded { get; set; }
    string? Message { get; set; }
    IList<string>? Errors { get; set; }
}

public interface IResult<out T> : IResult
{
    T? Data { get; }
}
