using System.Reflection;
using Application.Common.Behaviours;
using Application.Common.Mappings;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>))
            .AddLazyDi();
    }

    private static IServiceCollection AddLazyDi(this IServiceCollection services)
    {
        services.AddTransient(typeof(Lazy<>), typeof(LazyDi<>));
        return services;
    }

    public static void AddApplicationAutomapper(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddAutoMapper(config => { config.AddProfile(new MappingProfiles(assemblies)); });
    }

    // ReSharper disable once ConvertClosureToMethodGroup
    internal class LazyDi<T> : Lazy<T> where T : class
    {
        public LazyDi(IServiceProvider serviceProvider)
            : base(() => serviceProvider.GetRequiredService<T>())
        {
        }
    }
}