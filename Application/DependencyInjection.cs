using System.Reflection;
using Application.Common.Mappings;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }

    public static void AddApplicationAutomapper(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile(new MappingProfiles(assemblies));
        });
    }
}