namespace Shared.Core.Abstractions.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
}
