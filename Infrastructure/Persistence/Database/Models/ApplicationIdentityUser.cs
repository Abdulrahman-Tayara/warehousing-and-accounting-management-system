using Application.Common.Mappings;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Database.Models;

public class ApplicationIdentityUser : IdentityUser<int>, IMapFrom<User>, IDbModel
{
    
}