using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Repositories;

public class AccountRepository : RepositoryCrud<Account, AccountDb>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}