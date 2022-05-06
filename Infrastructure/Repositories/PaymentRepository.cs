using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentRepository : RepositoryCrud<Payment, PaymentDb>, IPaymentRepository
{
    public PaymentRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    protected override IQueryable<PaymentDb> GetIncludedDbSet()
    {
        return dbSet
            .Include(payment => payment.Currency)
            .Include(payment => payment.CurrencyAmounts);
    }
}