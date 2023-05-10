using Microsoft.AspNetCore.Identity;
using Modules.Identity.Core.Entities;
using Shared.Core.Abstractions.Services;

namespace Modules.Identity.Infrastructure.Services;

public class SystemUserService : ISystemUserService
{
    public SystemUserService(UserManager<User> userManager)
    {
        UserId = userManager.Users.FirstOrDefault(x => x.Email == "system@tgpro.com")?.Id ??
                 throw new NullReferenceException("System-Wide user is missing!");
    }

    public required string UserId { get; init; }
}
