using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Repositories;

public class PaymentRepository : RepositoryCrud<Payment, PaymentDb>, IPaymentRepository
{
    public PaymentRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}