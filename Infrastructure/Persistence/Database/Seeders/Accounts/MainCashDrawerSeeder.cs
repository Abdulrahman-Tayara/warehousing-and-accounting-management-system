using Application.Services.Settings;
using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Persistence.Database.Seeders.Accounts;

public class MainCashDrawerSeeder : ISeeder
{
    public void Seed(ApplicationDbContext dbContext, IApplicationSettingsProvider settingsProvider)
    {
        var settings = settingsProvider.Get();

        var mainCashDrawer = dbContext.Accounts
            .FirstOrDefault(account => account.Id == settings.DefaultMainCashDrawerAccountId);

        if (mainCashDrawer != null)
        {
            return;
        }
        
        using (var transaction = dbContext.Database.BeginTransaction())
        {
            var entry = dbContext.Accounts.Add(new AccountDb
            {
                Name = "Main Cash Drawer",
                Code = "MCD",
                City = "",
                Phone = ""
            });

            dbContext.SaveChanges();

            settings.DefaultMainCashDrawerAccountId = entry.Entity.Id;
            
            settingsProvider.Configure(settings);
            
            transaction.Commit();
        }
    }
}