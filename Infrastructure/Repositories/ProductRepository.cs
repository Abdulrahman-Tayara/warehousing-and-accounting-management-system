using Application.Exceptions;
using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : RepositoryCrud<Product, ProductDb>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<Product> FindIncludedByIdAsync(int id)
    {
        try
        {
            var productDb = await _GetQueryIncluded().FirstAsync();

            return MapModelToEntity(productDb);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.StackTrace);
            throw new NotFoundException();
        }
    }

    public IEnumerable<Product> GetAllIncluded()
    {
        return _GetQueryIncluded().ProjectTo<Product>(mapper.ConfigurationProvider);
    }

    private IQueryable<ProductDb> _GetQueryIncluded()
    {
        return dbSet
            .Include(p => p.Category)
            .Include(p => p.Currency)
            .Include(p => p.Manufacturer)
            .Include(p => p.Unit)
            .AsQueryable();
    }
}