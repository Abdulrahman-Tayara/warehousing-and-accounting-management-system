using Domain.Entities;

namespace Application.Repositories;

public interface IAccountRepository : IRepositoryCrud<Account, int>
{
}