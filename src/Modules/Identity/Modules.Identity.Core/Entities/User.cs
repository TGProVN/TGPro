using Microsoft.AspNetCore.Identity;
using Modules.Identity.Core.Enums;
using Shared.Model.Contracts;

namespace Modules.Identity.Core.Entities;

public class User : IdentityUser<string>, IAuditableEntity<string>
{
    public User()
    {
        UserTokens = new HashSet<UserToken>();
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string? AvatarId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public bool IsActive { get; set; }
    public Gender Gender { get; set; }

    public ICollection<UserToken> UserTokens { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTimeOffset? LastModifiedAt { get; set; }
}
