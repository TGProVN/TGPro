namespace Shared.Model.Contracts;

public interface IAuditableEntity<TId> : IAuditableEntity, IEntity<TId> {}

public interface IAuditableEntity : IEntity
{
    string CreatedBy { get; set; }
    DateTimeOffset CreatedAt { get; set; }
    string? LastModifiedBy { get; set; }
    DateTimeOffset? LastModifiedAt { get; set; }
}
