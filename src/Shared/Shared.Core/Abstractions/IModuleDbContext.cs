namespace Shared.Core.Abstractions;

public interface IModuleDbContext
{
    string? Schema { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
