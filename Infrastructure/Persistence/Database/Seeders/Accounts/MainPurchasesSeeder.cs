using Domain.Entities;
using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Persistence.Database.Seeders.Accounts;

public class MainPurchasesSeeder : ISeeder
{
    public void Seed(ApplicationDbContext dbContext)
    {
        var mainPurchases = dbContext.Accounts
            .FirstOrDefault(account => account.Type == AccountType.MainSales);

        if (mainPurchases == null)
        {
            dbContext.Accounts.Add(new AccountDb
            {
                Name = "Main Purchases",
                Code = "MPu",
                City = "",
                Phone = "",
                Type = AccountType.MainPurchases
            });
        }
    }
}