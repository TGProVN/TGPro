using Microsoft.AspNetCore.Identity;
using Shared.Model.Contracts;

namespace Modules.Identity.Core.Entities;

public class Role : IdentityRole, IAuditableEntity<string>
{
    public Role() : base()
    {
        RoleClaims = new HashSet<RoleClaim>();
    }
    
    public Role(string roleName, string? roleDescription = null) : base(roleName)
    {
        Description = roleDescription;
        RoleClaims = new HashSet<RoleClaim>();
    }
    
    public string? Description { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; }
    
    public ICollection<RoleClaim> RoleClaims { get; set; }
}
