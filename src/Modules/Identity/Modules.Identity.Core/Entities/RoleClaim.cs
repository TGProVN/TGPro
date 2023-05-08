using Microsoft.AspNetCore.Identity;
using Shared.Model.Contracts;

namespace Modules.Identity.Core.Entities;

public class RoleClaim : IdentityRoleClaim<string>, IAuditableEntity<int>
{
    public RoleClaim() : base()
    { }
    
    public RoleClaim(string? roleClaimDescription = null, string? roleClaimGroup = null)
    {
        Description = roleClaimDescription;
        Group = roleClaimGroup;
    }
    
    public string? Description { get; set; }
    public string? Group { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; }
    
    public virtual Role? Role { get; set; }
}
