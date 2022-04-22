using System.ComponentModel.DataAnnotations;
using Application.Common.Mappings;
using wms.Dto.Common;

namespace wms.Dto.Products;

public class ProductViewModel : IViewModel, IMapFrom<Domain.Entities.Product>
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public int CategoryId { get; set; }

    public int ManufacturerId { get; set; }

    public int UnitId { get; set; }

    public string Barcode { get; set; }

    public double Price { get; set; }
    
    public int CurrencyId { get; set; }
    
    public int MinLevel { get; set; }
}