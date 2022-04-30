using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Repositories;

public class CurrencyAmountRepository : RepositoryCrud<CurrencyAmount, CurrencyAmountDb>, ICurrencyAmountRepository
{
    public CurrencyAmountRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

}