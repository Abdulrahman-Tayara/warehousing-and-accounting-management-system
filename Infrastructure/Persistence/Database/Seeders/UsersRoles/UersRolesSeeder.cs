using Application.Services.Settings;
using Infrastructure.Persistence.Database.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Database.Seeders.UsersRoles;

public class UsersRolesSeeder : ISeeder
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationIdentityUser> _userManager;

    private const string AdminRole = "admin";
    private const string AdminUsername = "admin-user";
    private const string AdminPassword = "admin-pass-123";
    private const string AdminPermissions = "all";
    
    private const string TempRole = "temp";
    private const string TempUsername = "temp-user";
    private const string TempPassword = "temp-pass-123";
    private const string TempPermissions = "users-read,users-write";


    public UsersRolesSeeder(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationIdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task Seed(ApplicationDbContext dbContext, IApplicationSettingsProvider settingsProvider)
    {
        await _userManager.CreateAsync(
            new ApplicationIdentityUser
            {
                UserName = AdminUsername,
            },
            AdminPassword
        );
        
        await _roleManager.CreateAsync(
            new ApplicationRole
            {
                Name = AdminRole,
                NormalizedName = AdminRole.ToUpper(),
                Permissions = AdminPermissions
            }
        );

        var adminUser = await _userManager.FindByNameAsync(AdminUsername);
        await _userManager.AddToRoleAsync(adminUser, AdminRole);
        
        
        
        // Create Temp User
        
        await _userManager.CreateAsync(
            new ApplicationIdentityUser
            {
                UserName = TempUsername,
            },
            TempPassword
        );
        
        await _roleManager.CreateAsync(
            new ApplicationRole
            {
                Name = TempRole,
                NormalizedName = TempRole.ToUpper(),
                Permissions = TempPermissions
            }
        );

        var tempUser = await _userManager.FindByNameAsync(TempUsername);
        await _userManager.AddToRoleAsync(tempUser, TempRole);
    }
}