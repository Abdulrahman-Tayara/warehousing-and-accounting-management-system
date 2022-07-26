using System.Reflection;
using Application.Repositories;
using Application.Repositories.Aggregates;
using Application.Repositories.UnitOfWork;
using Application.Services.Events;
using Application.Services.Identity;
using Application.Services.Settings;
using Application.Settings;
using Infrastructure.Persistence.Database;
using Infrastructure.Persistence.Database.Models;
using Infrastructure.Persistence.Database.Seeders;
using Infrastructure.Persistence.Database.Triggers;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Aggregates;
using Infrastructure.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase("wms_db"); });
        else
        {
            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseTriggers(triggersOptions => triggersOptions.AddTrigger<SoftDeleteTrigger>());

                    options.UseSqlServer(
                        configuration.GetValue<bool>("UseLocalDatabaseServer")
                            ? configuration.GetConnectionString("LocalConnection")
                            : configuration.GetConnectionString("DefaultConnection")
                    );
                },
                ServiceLifetime.Transient
            );
        }

        services.AddUserIdentityServer();
        services.AddRepositories();
        services.AddServices();
        services.AddApplicationSettings();
        return services;
    }

    private static void AddUserIdentityServer(this IServiceCollection services)
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

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
        services.AddScoped<IUnitRepository, UnitRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<ICountryOriginRepository, CountryOriginRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IStoragePlaceRepository, StoragePlaceRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IProductMovementRepository, ProductMovementRepository>();
        services.AddScoped<ICurrencyAmountRepository, CurrencyAmountRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IJournalRepository, JournalRepository>();
        services.AddScoped<IConversionRepository, ConversionRepository>();

        services.AddScoped<IInvoicePaymentsRepository, InvoicePaymentsRepository>();

        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IEventPublisherService, EventPublisherService>();
    }

    private static void AddApplicationSettings(this IServiceCollection services)
    {
        services.AddScoped<IApplicationSettingsProvider, ApplicationSettingsProvider>();
        services.AddScoped<ApplicationSettings>(s => s.GetService<IApplicationSettingsProvider>()!.Get());
    }

    public static void EnsureDatabaseOps(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbSeeder = services.GetRequiredService<IDatabaseSeeder>();
            var settingsProvider = services.GetRequiredService<IApplicationSettingsProvider>();

            using (var dbContext = services.GetRequiredService<ApplicationDbContext>())
            {
                dbContext.Database.Migrate();
                dbContext.Database.EnsureCreated();
                dbContext.Seed(dbSeeder, settingsProvider);
            }
        }
    }
}