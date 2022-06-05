using Domain.Entities;
using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Persistence.Database.Seeders.Accounts;

public class MainCashDrawerSeeder : ISeeder
{
    public void Seed(ApplicationDbContext dbContext)
    {
        var mainCashDrawer = dbContext.Accounts
            .FirstOrDefault(account => account.Type == AccountType.MainCashDrawer);

        if (mainCashDrawer == null)
        {
            dbContext.Accounts.Add(new AccountDb
            {
                Name = "Main Cash Drawer",
                Code = "MCD",
                City = "",
                Phone = "",
                Type = AccountType.MainCashDrawer
            });
        }
    }
}