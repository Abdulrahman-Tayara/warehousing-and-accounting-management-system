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
    
    public void Seed(ApplicationDbContext dbContext, IApplicationSettingsProvider settingsProvider)
    {
        _seeders.ForEach(seeder => seeder.Seed(dbContext, settingsProvider));
    }
}