using System.ComponentModel.DataAnnotations;
using Application.Commands.Products;
using Application.Common.Mappings;

namespace wms.Dto.Products;

public class CreateProductRequest : IMapFrom<CreateProductCommand>
{
    [Required] public string Name { get; set; }

    [Required] public int CategoryId { get; set; }

    [Required] public int ManufacturerId { get; set; }

    [Required] public int UnitId { get; set; }

    [Required] public string Barcode { get; set; }

    [Required] public double Price { get; set; }
    
    [Required] public int CurrencyId { get; set; }
}