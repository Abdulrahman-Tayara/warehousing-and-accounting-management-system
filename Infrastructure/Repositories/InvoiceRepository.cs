using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class InvoiceRepository : RepositoryCrud<Invoice, InvoiceDb>, IInvoiceRepository
{
    public InvoiceRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    protected override IQueryable<InvoiceDb> GetIncludedDbSet()
    {
        return dbSet.Include(i => i.Items)
            .ThenInclude(item => item.CurrencyAmounts!.Where(c => c.Key.Equals(CurrencyAmountKey.Movement)))
            .ThenInclude(c => c.Currency)
            .Include(i => i.Items)
            .ThenInclude(item => item.Product)
            .Include(i => i.Items)
            .ThenInclude(item => item.Currency);
    }
}