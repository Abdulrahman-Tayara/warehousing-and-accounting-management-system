using Domain.Entities;

namespace Application.Repositories;

public interface IUserRepository : IRepositoryCrud<User, int>
{
}