using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Modules.Identity.Core.Abstractions.Services;
using Modules.Identity.Core.Entities;
using Modules.Identity.Infrastructure.Contexts;
using Modules.Identity.Infrastructure.Extensions;
using Shared.Core.Configurations;
using Shared.Core.Constants;
using Shared.Core.Helpers;

namespace Modules.Identity.Infrastructure.Services;

public class IdentitySeeder : IIdentitySeeder
{
    private readonly ILogger<IdentitySeeder> _logger;
    private readonly AppIdentityDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly AppConfiguration _appConfig;
    private string _adminId;

    public IdentitySeeder(
        ILogger<IdentitySeeder> logger,
        AppIdentityDbContext dbContext,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IOptions<AppConfiguration> appConfig
    )
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _appConfig = appConfig.Value;
        _adminId = string.Empty;
    }

    public void Run()
    {
        try
        {
            // @TODO - Check DbSet<>

            SeedAdministrator();
            SeedSystemAccount();

            var result = _dbContext.SaveChanges();

            if (result > 0)
            {
                _logger.LogInformation("Done!");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        }
    }

    private void SeedAdministrator()
    {
        Task.Run(async () => {
                 _logger.LogInformation("Initialize Administrator Role...");

                 var adminRole = new Role(AppConstants.Roles.Administrator, "Full permissions role");
                 var roleFromDb = await _roleManager.FindByNameAsync(AppConstants.Roles.Administrator);

                 if (roleFromDb is null)
                 {
                     await _roleManager.CreateAsync(adminRole);
                     roleFromDb = await _roleManager.FindByNameAsync(AppConstants.Roles.Administrator);

                     if (roleFromDb is null)
                     {
                         _logger.LogError("An error has occurred: Failed to initialize administrator role");
                         return;
                     }

                     _logger.LogInformation("Done!");
                 }

                 _logger.LogInformation("Initialize Default Web Owner...");

                 var webOwner = new User {
                     FirstName = "Vo",
                     LastName = "Thuong",
                     Email = "trungthuongvo109@gmail.com",
                     UserName = "trungthuongvo109",
                     AvatarUrl =
                         "https://res.cloudinary.com/tgproimagecloud/image/upload/v1638705794/TGProV3/users/admin_avatar.jpg",
                     AvatarId = "TGProV3/users/admin_avatar",
                     EmailConfirmed = true,
                     PhoneNumber = "0375274267",
                     PhoneNumberConfirmed = true,
                     IsActive = true
                 };

                 var ownerFromDb = await _userManager.FindByEmailAsync(webOwner.Email);

                 if (ownerFromDb is null)
                 {
                     await _userManager.CreateAsync(webOwner, _appConfig.DefaultAppPassword);

                     ownerFromDb = await _userManager.FindByEmailAsync(webOwner.Email);

                     if (ownerFromDb is null)
                     {
                         _logger.LogError("An error has occurred: Failed to initialize default web owner");
                         return;
                     }

                     _adminId = ownerFromDb.Id;
                     ownerFromDb.CreatedBy = _adminId;
                     roleFromDb.CreatedBy = _adminId;

                     await _userManager.UpdateAsync(ownerFromDb);
                     await _roleManager.UpdateAsync(roleFromDb);

                     var result = await _userManager.AddToRoleAsync(webOwner, AppConstants.Roles.Administrator);

                     if (result.Succeeded)
                     {
                         _logger.LogInformation("Done!");
                     }
                     else
                     {
                         foreach (var error in result.Errors)
                         {
                             _logger.LogError("An error has occurred: {description}", error.Description);
                         }
                     }
                 }

                 foreach (var permissionDetail in PermissionHelpers.GetAllAppPermissions())
                 {
                     await _dbContext.AddPermissionClaim(roleFromDb, permissionDetail);
                 }
             })
            .GetAwaiter()
            .GetResult();
    }

    private void SeedSystemAccount()
    {
        Task.Run(async () => {
                 _logger.LogInformation("Initialize System Role...");

                 var systemRole = new Role(AppConstants.Roles.System, "Full permissions role");
                 var roleFromDb = await _roleManager.FindByNameAsync(AppConstants.Roles.System);

                 if (roleFromDb is null)
                 {
                     await _roleManager.CreateAsync(systemRole);
                     roleFromDb = await _roleManager.FindByNameAsync(AppConstants.Roles.System);

                     if (roleFromDb is null)
                     {
                         _logger.LogError("An error has occurred: Failed to initialize system role");
                         return;
                     }

                     _logger.LogInformation("Done!");
                 }

                 _logger.LogInformation("Initialize Default System-Wide Account...");

                 var systemWide = new User {
                     FirstName = "Account",
                     LastName = "System",
                     Email = "system@tgpro.com",
                     UserName = "tgpro_system",
                     AvatarUrl =
                         "https://res.cloudinary.com/tgproimagecloud/image/upload/v1683690719/TGProV3/users/system_avatar.jpg",
                     AvatarId = "TGProV3/users/system_avatar.jpg",
                     EmailConfirmed = true,
                     PhoneNumber = "0000000000",
                     PhoneNumberConfirmed = true,
                     IsActive = true
                 };

                 var systemWideFromDb = await _userManager.FindByEmailAsync(systemWide.Email);

                 if (systemWideFromDb is null)
                 {
                     await _userManager.CreateAsync(systemWide, _appConfig.DefaultAppPassword);

                     systemWideFromDb = await _userManager.FindByEmailAsync(systemWide.Email);

                     if (systemWideFromDb is null)
                     {
                         _logger.LogError("An error has occurred: Failed to initialize default system-wide account");
                         return;
                     }

                     systemWide.CreatedBy = _adminId;
                     roleFromDb.CreatedBy = _adminId;

                     await _userManager.UpdateAsync(systemWideFromDb);
                     await _roleManager.UpdateAsync(roleFromDb);

                     var result = await _userManager.AddToRoleAsync(systemWide, AppConstants.Roles.System);

                     if (result.Succeeded)
                     {
                         _logger.LogInformation("Done!");
                     }
                     else
                     {
                         foreach (var error in result.Errors)
                         {
                             _logger.LogError("An error has occurred: {description}", error.Description);
                         }
                     }
                 }

                 foreach (var permissionDetail in PermissionHelpers.GetDefaultAppPermissions())
                 {
                     await _dbContext.AddPermissionClaim(roleFromDb, permissionDetail);
                 }
             })
            .GetAwaiter()
            .GetResult();
    }
}
