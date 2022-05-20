using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Infrastructure.Repositories;

public class PaymentRepository : RepositoryCrud<Payment, PaymentDb>, IPaymentRepository
{
    public PaymentRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    protected override IQueryable<PaymentDb> GetIncludedDbSet()
    {
        return DbSet
            .Include(payment => payment.Currency)
            .IncludeFilter(payment => payment.CurrencyAmounts.Where(p => p.Key == CurrencyAmountKey.Payment));
    }
}