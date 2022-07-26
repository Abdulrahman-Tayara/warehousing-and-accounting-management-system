using Application.Services.Settings;
using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Persistence.Database.Seeders.Accounts;

public class ConversionsSeeder : ISeeder
{
    public Task Seed(ApplicationDbContext dbContext, IApplicationSettingsProvider settingsProvider)
    {
        var settings = settingsProvider.Get();

        var conversionsAccount = dbContext.Accounts
            .FirstOrDefault(account => account.Id == settings.DefaultConversionsAccountId);
        
        if (conversionsAccount != null)
        {
            return Task.CompletedTask;
        }
        
        using (var transaction = dbContext.Database.BeginTransaction())
        {
            var entry = dbContext.Accounts.Add(new AccountDb
            {
                Name = "التحويلات",
                Code = "Co",
                City = "",
                Phone = ""
            });

            dbContext.SaveChanges();

            settings.DefaultConversionsAccountId = entry.Entity.Id;
            
            settingsProvider.Configure(settings);
            
            transaction.Commit();
        }
        
        return Task.CompletedTask;
    }
}