using Domain.Events;

namespace Domain.Entities;

public class Product : BaseEntity<int>, IHasDomainEvents
{
    public string Name { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public int ManufacturerId { get; set; }
    public Manufacturer? Manufacturer { get; set; }

    public int CountryOriginId { get; set; }
    public CountryOrigin? CountryOrigin { get; set; }

    public int UnitId { get; set; }
    public Unit? Unit { get; set; }

    public string Barcode { get; set; }

    public double Price { get; set; }

    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }

    public int MinLevel { get; set; }

    public void UpdateMinLevel(int value)
    {
        if (MinLevel == value)
        {
            return;
        }

        int oldMinLevel = MinLevel;
        MinLevel = value;
        Events.Add(new ProductMinLevelUpdated(Id, oldMinLevel, MinLevel));
    }

    public Product(int id, string name, int categoryId, int manufacturerId, int countryOriginId, int unitId,
        string barcode, double price, int currencyId, int minLevel)
    {
        Id = id;
        Name = name;
        CategoryId = categoryId;
        ManufacturerId = manufacturerId;
        CountryOriginId = countryOriginId;
        UnitId = unitId;
        Barcode = barcode;
        Price = price;
        CurrencyId = currencyId;
        MinLevel = minLevel;
    }

    public Product(int id, string name, int categoryId, Category? category, int manufacturerId,
        Manufacturer? manufacturer, int countryOriginId, CountryOrigin? countryOrigin, int unitId, Unit? unit,
        string barcode, double price, int currencyId, Currency? currency)
    {
        Id = id;
        Name = name;
        CategoryId = categoryId;
        Category = category;
        ManufacturerId = manufacturerId;
        Manufacturer = manufacturer;
        CountryOriginId = countryOriginId;
        CountryOrigin = countryOrigin;
        UnitId = unitId;
        Unit = unit;
        Barcode = barcode;
        Price = price;
        CurrencyId = currencyId;
        Currency = currency;
    }

    public bool HasMinLevel => MinLevel > 0;

    public IList<DomainEvent> Events { get; set; } = new List<DomainEvent>();
}