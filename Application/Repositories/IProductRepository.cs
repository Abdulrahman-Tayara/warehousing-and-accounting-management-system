using Domain.Entities;

namespace Application.Repositories;

public interface IProductRepository : IRepositoryCrud<Product, int>
{
    IQueryable<Product> GetAllWithNewMinLevelWarnings(int invoiceId);
    
    IQueryable<Product> GetAllWithNewMinLevelResolved(int invoiceId);

    IQueryable<Product> GetAllInStoragePlace(int storagePlaceId, bool includeStoragePlaceChildren = false);
}