using Application.Common.Mappings;
using Application.Common.Security;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Database.Models;

public class ApplicationRole : IdentityRole<int>, IMapFrom<Role>
{
    public string Permissions { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<string, Permissions>().ConvertUsing(s => Application.Common.Security.Permissions.From(s));
        profile.CreateMap<Permissions, string>().ConvertUsing(p => p.ToString());
        profile.CreateMap<Role, ApplicationRole>();
        profile.CreateMap<ApplicationRole, Role>();
    }
}