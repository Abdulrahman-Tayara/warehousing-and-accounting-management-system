using Application.Services.Settings;

namespace Infrastructure.Persistence.Database.Seeders.Currencies;

public class CurrenciesSeeder : ISeeder
{
    private readonly List<ISeeder> _seeders;

    public CurrenciesSeeder()
    {
        _seeders = new List<ISeeder>
        {
            new MainCurrencySeeder()
        };
    }
    
    public Task Seed(ApplicationDbContext dbContext, IApplicationSettingsProvider settingsProvider)
    {
        return Task.WhenAll(_seeders.Select(seeder => seeder.Seed(dbContext, settingsProvider)));
    }
}