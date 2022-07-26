using Application.Services.Settings;
using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Persistence.Database.Seeders.Accounts;

public class MainImportsSeeder : ISeeder
{
    public void Seed(ApplicationDbContext dbContext, IApplicationSettingsProvider settingsProvider)
    {
        var settings = settingsProvider.Get();

        var mainImports = dbContext.Accounts
            .FirstOrDefault(account => account.Id == settings.DefaultMainImportsAccountId);
        
        if (mainImports != null)
        {
            return;
        }
        
        using (var transaction = dbContext.Database.BeginTransaction())
        {
            var entry = dbContext.Accounts.Add(new AccountDb
            {
                Name = "الواردات الرئيسي",
                Code = "MIm",
                City = "",
                Phone = ""
            });

            dbContext.SaveChanges();

            settings.DefaultMainImportsAccountId = entry.Entity.Id;
            
            settingsProvider.Configure(settings);
            
            transaction.Commit();
        }
    }
}