using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StoragePlacesRepository : RepositoryCrud<StoragePlace, StoragePlaceDb>, IStoragePlaceRepository
{
    public StoragePlacesRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
    
    protected override IQueryable<StoragePlaceDb> GetIncludedDbSet()
    {
        return dbSet
            .Include(p => p.Container)
            .Include(p => p.Warehouse);
    }
}