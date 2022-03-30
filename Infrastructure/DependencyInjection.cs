using Infrastructure.Persistence.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
        return services;
    }
}