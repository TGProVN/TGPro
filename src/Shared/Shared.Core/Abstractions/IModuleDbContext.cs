namespace Shared.Core.Abstractions;

public interface IModuleDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
