using Application.Repositories;
using AutoMapper;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;

namespace Infrastructure.Repositories;

public class ManufacturerRepository : RepositoryCrud<Domain.Entities.Manufacturer, ManufacturerDb>, IManufacturerRepository
{
    public ManufacturerRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}