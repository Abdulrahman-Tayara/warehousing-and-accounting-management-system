using Application.Services.Settings;
using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Persistence.Database.Seeders.Accounts;

public class MainCashDrawerSeeder : ISeeder
{
    public Task Seed(ApplicationDbContext dbContext, IApplicationSettingsProvider settingsProvider)
    {
        var settings = settingsProvider.Get();

        var mainCashDrawer = dbContext.Accounts
            .FirstOrDefault(account => account.Id == settings.DefaultMainCashDrawerAccountId);

        if (mainCashDrawer != null)
        {
            return Task.CompletedTask;
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
        
        return Task.CompletedTask;
    }
}