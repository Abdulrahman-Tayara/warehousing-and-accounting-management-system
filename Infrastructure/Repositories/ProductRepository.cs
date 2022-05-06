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
    
    protected override IQueryable<ProductDb> GetIncludedDbSet()
    {
        return DbSet
            .Include(p => p.Category)
            .Include(p => p.Currency)
            .Include(p => p.Manufacturer)
            .Include(p => p.Unit);
    }
}