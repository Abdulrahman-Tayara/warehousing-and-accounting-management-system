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
        return base.GetIncludedDbSet()
            .Include(invoice => invoice.Account)
            .Include(invoice => invoice.Currency)
            .Include(invoice => invoice.Warehouse);
    }
}