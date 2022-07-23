using Application.Services.Settings;

namespace Infrastructure.Persistence.Database.Seeders.Accounts;

public class AccountsSeeder : ISeeder
{
    private readonly List<ISeeder> _seeders;

    public AccountsSeeder()
    {
        _seeders = new List<ISeeder>
        {
            new MainCashDrawerSeeder(),
            new MainSalesSeeder(),
            new MainPurchasesSeeder()
        };
    }
    
    public Task Seed(ApplicationDbContext dbContext, IApplicationSettingsProvider settingsProvider)
    {
        return Task.WhenAll(_seeders.Select(seeder => seeder.Seed(dbContext, settingsProvider)));
    }
}