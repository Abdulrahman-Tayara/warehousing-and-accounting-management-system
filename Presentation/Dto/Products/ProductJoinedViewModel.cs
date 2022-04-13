using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Common;

namespace wms.Dto.Products;

public class ProductJoinedViewModel : IViewModel, IMapFrom<Product>
{
    public string Name { get; set; }

    public Category Category { get; set; }

    public Manufacturer Manufacturer { get; set; }

    public Unit Unit { get; set; }

    public string Barcode { get; set; }

    public double Price { get; set; }

    public Currency Currency { get; set; }
}