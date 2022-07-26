using Application.Services.Settings;
using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Persistence.Database.Seeders.Accounts;

public class MainExportsSeeder : ISeeder
{
    public Task Seed(ApplicationDbContext dbContext, IApplicationSettingsProvider settingsProvider)
    {
        var settings = settingsProvider.Get();

        var mainExports = dbContext.Accounts
            .FirstOrDefault(account => account.Id == settings.DefaultMainExportsAccountId);
        
        if (mainExports != null)
        {
            return Task.CompletedTask;
        }
        
        using (var transaction = dbContext.Database.BeginTransaction())
        {
            var entry = dbContext.Accounts.Add(new AccountDb
            {
                Name = "الصادرات الرئيسي",
                Code = "MEx",
                City = "",
                Phone = ""
            });

            dbContext.SaveChanges();

            settings.DefaultMainExportsAccountId = entry.Entity.Id;
            
            settingsProvider.Configure(settings);
            
            transaction.Commit();
        }
        
        return Task.CompletedTask;
    }
}