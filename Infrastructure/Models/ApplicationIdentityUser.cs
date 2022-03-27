using Application.Common.Mappings;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models;

public class ApplicationIdentityUser : IdentityUser<int>, IMapFrom<User>
{
    
}