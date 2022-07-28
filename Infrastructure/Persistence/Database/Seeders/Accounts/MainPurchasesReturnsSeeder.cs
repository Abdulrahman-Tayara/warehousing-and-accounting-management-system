using Application.Services.Settings;
using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Persistence.Database.Seeders.Accounts;

public class MainPurchasesReturnsSeeder : ISeeder
{
    public Task Seed(ApplicationDbContext dbContext, IApplicationSettingsProvider settingsProvider)
    {
        var settings = settingsProvider.Get();

        var mainPurchasesReturns = dbContext.Accounts
            .FirstOrDefault(account => account.Id == settings.DefaultPurchasesReturnsAccountId);
        
        if (mainPurchasesReturns != null)
        {
            return Task.CompletedTask;
        }
        
        using (var transaction = dbContext.Database.BeginTransaction())
        {
            var entry = dbContext.Accounts.Add(new AccountDb
            {
                Name = "مرتجعات مشتريات",
                Code = "MRPu",
                City = "",
                Phone = ""
            });

            dbContext.SaveChanges();

            settings.DefaultPurchasesReturnsAccountId = entry.Entity.Id;
            
            settingsProvider.Configure(settings);
            
            transaction.Commit();
        }
        
        return Task.CompletedTask;
    }
}