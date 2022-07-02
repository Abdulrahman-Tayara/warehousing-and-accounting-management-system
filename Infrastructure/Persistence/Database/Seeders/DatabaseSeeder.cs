using Application.Services.Settings;
using Infrastructure.Persistence.Database.Models;
using Infrastructure.Persistence.Database.Seeders.Accounts;
using Infrastructure.Persistence.Database.Seeders.Currencies;
using Infrastructure.Persistence.Database.Seeders.UsersRoles;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Database.Seeders;

public interface IDatabaseSeeder : ISeeder
{
}

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationIdentityUser> _userManager;
    private readonly List<ISeeder> _seeders;

    public DatabaseSeeder(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationIdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _seeders = new List<ISeeder>
        {
            new AccountsSeeder(),
            new CurrenciesSeeder(),
            new UsersRolesSeeder(roleManager, userManager)
        };
    }

    public Task Seed(ApplicationDbContext dbContext, IApplicationSettingsProvider settingsProvider)
    {
        return Task.WhenAll(_seeders.Select(seeder => seeder.Seed(dbContext, settingsProvider)));
    }
}