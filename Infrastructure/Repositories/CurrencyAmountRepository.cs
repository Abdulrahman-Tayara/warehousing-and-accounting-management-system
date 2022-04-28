using Application.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class CurrencyAmountRepository : ICurrencyAmountRepository
{
    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task<SaveAction<Task<CurrencyAmount>>> CreateAsync(CurrencyAmount entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<CurrencyAmount> GetAll(GetAllOptions<CurrencyAmount>? options = default)
    {
        throw new NotImplementedException();
    }

    public Task<CurrencyAmount> FindByIdAsync(int id, FindOptions? options = default)
    {
        throw new NotImplementedException();
    }

    public Task<CurrencyAmount> Update(CurrencyAmount entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<SaveAction<Task<IEnumerable<CurrencyAmount>>>> CreateAllAsync(IEnumerable<CurrencyAmount> entities)
    {
        throw new NotImplementedException();
    }
}