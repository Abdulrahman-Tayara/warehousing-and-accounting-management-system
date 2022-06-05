using Domain.Entities;
using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Persistence.Database.Seeders.Accounts;

public class MainSalesSeeder : ISeeder
{
    public void Seed(ApplicationDbContext dbContext)
    {
        var mainSales = dbContext.Accounts
            .FirstOrDefault(account => account.Type == AccountType.MainSales);

        if (mainSales == null)
        {
            dbContext.Accounts.Add(new AccountDb
            {
                Name = "Main Sales",
                Code = "MSa",
                City = "",
                Phone = "",
                Type = AccountType.MainSales
            });
        }
    }
}