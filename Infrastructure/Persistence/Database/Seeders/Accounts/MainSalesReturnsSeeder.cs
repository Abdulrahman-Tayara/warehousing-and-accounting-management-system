using Application.Services.Settings;
using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Persistence.Database.Seeders.Accounts;

public class MainSalesReturnsSeeder : ISeeder
{
    public Task Seed(ApplicationDbContext dbContext, IApplicationSettingsProvider settingsProvider)
    {
        var settings = settingsProvider.Get();

        var mainSalesReturns = dbContext.Accounts
            .FirstOrDefault(account => account.Id == settings.DefaultSalesReturnsAccountId);
        
        if (mainSalesReturns != null)
        {
            return Task.CompletedTask;
        }
        
        using (var transaction = dbContext.Database.BeginTransaction())
        {
            var entry = dbContext.Accounts.Add(new AccountDb
            {
                Name = "مرتجعات مبيعات",
                Code = "MRPu",
                City = "",
                Phone = ""
            });

            dbContext.SaveChanges();

            settings.DefaultSalesReturnsAccountId = entry.Entity.Id;
            
            settingsProvider.Configure(settings);
            
            transaction.Commit();
        }
        
        return Task.CompletedTask;
    }
}