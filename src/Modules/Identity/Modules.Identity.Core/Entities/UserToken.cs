using Microsoft.AspNetCore.Identity;
using Shared.Model.Contracts;

namespace Modules.Identity.Core.Entities;

public class UserToken : IdentityUserToken<string>, IEntity<string>
{
    public string Id { get; set; } = string.Empty;
    public DateTimeOffset Expires { get; } = DateTimeOffset.Now.AddDays(7);
    public DateTimeOffset? Revoked { get; set; }
    
    public bool IsExpired {
        get => DateTimeOffset.Now >= Expires;
    }
    
    public bool IsActive {
        get => Revoked == null && !IsExpired;
    }

    public virtual User? User { get; set; }
}
