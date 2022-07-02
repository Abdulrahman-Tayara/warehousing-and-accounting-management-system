using Application.Services.Settings;

using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Persistence.Database.Seeders.Accounts;

public class MainPurchasesSeeder : ISeeder
{
    public Task Seed(ApplicationDbContext dbContext, IApplicationSettingsProvider settingsProvider)
    {
        var settings = settingsProvider.Get();

        var mainPurchases = dbContext.Accounts
            .FirstOrDefault(account => account.Id == settings.DefaultPurchasesAccountId);
        
        if (mainPurchases != null)
        {
            return Task.CompletedTask;
        }
        
        using (var transaction = dbContext.Database.BeginTransaction())
        {
            var entry = dbContext.Accounts.Add(new AccountDb
            {
                Name = "Main Purchases",
                Code = "MPu",
                City = "",
                Phone = ""
            });

            dbContext.SaveChanges();

            settings.DefaultPurchasesAccountId = entry.Entity.Id;
            
            settingsProvider.Configure(settings);
            
            transaction.Commit();
        }
        
        return Task.CompletedTask;
    }
}