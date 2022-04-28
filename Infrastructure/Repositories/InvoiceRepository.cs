using Application.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Task<SaveAction<Task<Invoice>>> CreateAsync(Invoice entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Invoice> GetAll(GetAllOptions<Invoice>? options = default)
    {
        throw new NotImplementedException();
    }

    public Task<Invoice> FindByIdAsync(int id, FindOptions? options = default)
    {
        throw new NotImplementedException();
    }

    public Task<Invoice> Update(Invoice entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<SaveAction<Task<IEnumerable<Invoice>>>> CreateAllAsync(IEnumerable<Invoice> entities)
    {
        throw new NotImplementedException();
    }
}