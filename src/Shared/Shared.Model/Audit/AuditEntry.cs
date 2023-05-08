using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Core.Enums;
using System.Text.Json;

namespace Shared.Model.Audit;

public class AuditEntry
{
    public EntityEntry Entry;
    
    public AuditEntry(EntityEntry entry)
    {
        Entry = entry;
        KeyValues = new Dictionary<string, object>();
        OldValues = new Dictionary<string, object>();
        NewValues = new Dictionary<string, object>();
        TemporaryProperties = new List<PropertyEntry>();
        ChangedColumns = new List<string>();
    }
    
    public string? UserId { get; init; }
    public string? TableName { get; init; }
    public AuditType AuditType { get; set; }
    public Dictionary<string, object> KeyValues { get; }
    public Dictionary<string, object> OldValues { get; }
    public Dictionary<string, object> NewValues { get; }
    public ICollection<PropertyEntry> TemporaryProperties { get; }
    public ICollection<string> ChangedColumns { get; }
    
    public bool HasTemporaryProperties {
        get => TemporaryProperties.Any();
    }
    
    public Audit ToAudit()
    {
        return new Audit {
            UserId = UserId,
            Type = AuditType.ToString(),
            TableName = TableName,
            DateTime = DateTime.UtcNow,
            PrimaryKey = JsonSerializer.Serialize(KeyValues),
            OldValues = OldValues.Count == 0 ? null : JsonSerializer.Serialize(OldValues),
            NewValues = NewValues.Count == 0 ? null : JsonSerializer.Serialize(NewValues),
            AffectedColumns = ChangedColumns.Count == 0 ? null : JsonSerializer.Serialize(ChangedColumns)
        };
    }
}
