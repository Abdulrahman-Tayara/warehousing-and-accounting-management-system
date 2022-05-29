using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Common;

namespace wms.Dto.Products;

public class ProductJoinedViewModel : IViewModel, IMapFrom<Product>
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public Category Category { get; set; }

    public Manufacturer Manufacturer { get; set; }
    
    public CountryOrigin CountryOrigin { get; set; }

    public Unit Unit { get; set; }

    public string Barcode { get; set; }

    public double Price { get; set; }

    public Currency Currency { get; set; }
    
    public int MinLevel { get; set; }
}