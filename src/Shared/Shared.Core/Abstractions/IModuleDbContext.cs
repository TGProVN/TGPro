namespace Shared.Core.Abstractions;

public interface IModuleDbContext
{
    string? Schema { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
