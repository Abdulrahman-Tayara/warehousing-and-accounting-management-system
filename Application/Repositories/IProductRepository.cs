using Domain.Entities;

namespace Application.Repositories;

public interface IProductRepository : IRepositoryCrud<Product, int>
{
    public Task<Product> FindIncludedByIdAsync(int id);
    
    public IEnumerable<Product> GetAllIncluded();
}