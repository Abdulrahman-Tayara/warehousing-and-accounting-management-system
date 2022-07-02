using Application.Services.Events;
using Domain.Events;
using Infrastructure.Persistence.Database.Models;
using Infrastructure.Persistence.Database.Models.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Database;

public class ApplicationDbContext : IdentityDbContext<ApplicationIdentityUser, ApplicationRole, int>
{
    public DbSet<ManufacturerDb> Manufacturers { get; set; } = null!;
    public DbSet<CategoryDb> Categories { get; set; } = null!;
    public DbSet<UnitDb> Units { get; set; } = null!;
    public DbSet<CurrencyDb> Currencies { get; set; } = null!;
    public DbSet<CountryOriginDb> CountryOrigins { get; set; } = null!;
    public DbSet<ApplicationSettingDb> Settings { get; set; } = null!;
    public DbSet<ProductDb> Products { get; set; } = null!;
    public DbSet<WarehouseDb> Warehouses { get; set; }
    public DbSet<StoragePlaceDb> StoragePlaces { get; set; }
    public DbSet<AccountDb> Accounts { get; set; } = null!;
    public DbSet<JournalDb> JournalDbs { get; set; }

    public DbSet<ProductMovementDb> ProductMovements { get; set; }
    public DbSet<CurrencyAmountDb> CurrencyAmounts { get; set; }
    public DbSet<InvoiceDb> Invoices { get; set; }
    public DbSet<PaymentDb> Payments { get; set; }
    public DbSet<NotificationDb> Notifications { get; set; }

    private readonly IEventPublisherService _eventPublisher;
    
    public ApplicationDbContext(DbContextOptions options, IEventPublisherService eventPublisher) : base(options)
    {
        _eventPublisher = eventPublisher;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyGlobalFilters<ISoftDeletable>(e => !e.IsDeleted);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var events = ChangeTracker.Entries<IHasDomainEvents>()
            .Select(x => x.Entity.Events)
            .SelectMany(x => x)
            .Where(domainEvent => !domainEvent.IsPublished)
            .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);

        return result;
    }

    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _eventPublisher.Publish(@event);
        }
    }
}