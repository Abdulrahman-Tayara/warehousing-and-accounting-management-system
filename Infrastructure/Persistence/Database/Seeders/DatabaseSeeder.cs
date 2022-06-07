using Application.Services.Settings;
using Infrastructure.Persistence.Database.Seeders.Accounts;
using Infrastructure.Persistence.Database.Seeders.Currencies;

namespace Infrastructure.Persistence.Database.Seeders;

public interface IDatabaseSeeder : ISeeder
{
}

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly List<ISeeder> _seeders;

    public DatabaseSeeder()
    {
        _seeders = new List<ISeeder>
        {
            new AccountsSeeder(),
            new CurrenciesSeeder()
        };
    }

    public void Seed(ApplicationDbContext dbContext, IApplicationSettingsProvider settingsProvider)
    {
        _seeders.ForEach(seeder => seeder.Seed(dbContext, settingsProvider));
    }
}