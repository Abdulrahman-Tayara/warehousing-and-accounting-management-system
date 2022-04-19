using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Repositories;

public class WarehouseRepository : RepositoryCrud<Warehouse, WarehouseDb>, IWarehouseRepository
{
    public WarehouseRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
    
}