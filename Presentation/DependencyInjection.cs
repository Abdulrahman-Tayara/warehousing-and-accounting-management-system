using System.Text.Json.Serialization;
using Application.Services.Identity;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using wms.Dto.Common.Responses.Validation;
using wms.Filters;
using wms.Services;
using wms.Utils;

namespace wms;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationControllers(this IServiceCollection services)
    {
        services
            .AddControllers(options => { options.Filters.Add<ExceptionFilter>(); })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory =
                    actionContext => new BadRequestObjectResult(actionContext.ModelState);
            });

        return services;
    }

    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        return services;
    }

    public static IServiceCollection AddCurrentUserService(this IServiceCollection services)
    {
        return services.AddScoped<ICurrentUserService, ApiCurrentUserService>();
    }
    
    public static void UseSwaggerMiddlewares(this WebApplication app)
    {
        // if (app.Environment.IsDevelopment())
        // {
        app.UseSwagger();
        app.UseSwaggerUI(options => options.DocExpansion(DocExpansion.None));
        // }
    }
}