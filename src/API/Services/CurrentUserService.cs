using Shared.Core.Abstractions.Services;
using System.Security.Claims;

namespace API.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
    
    public string? UserId { get; }
}
