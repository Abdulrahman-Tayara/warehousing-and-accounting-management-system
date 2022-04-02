using Application.Repositories;
using Application.Services.Identity;
using Infrastructure.Models;
using Infrastructure.Persistence.Database;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase("wms_db"); });
        else
            services.AddSqlServer<ApplicationDbContext>(configuration.GetConnectionString("DefaultConnection"));

        services.AddUserIdentityServer();
        services.AddRepositories();
        services.AddServices();

        return services;
    }

    static void AddUserIdentityServer(this IServiceCollection services)
    {
        services.AddDefaultIdentity<ApplicationIdentityUser>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }

    static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }

    static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();
    }
}