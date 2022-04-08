using System.ComponentModel.DataAnnotations;
using Application.Commands.Products;
using Application.Common.Mappings;

namespace wms.Dto.Products;

public class UpdateProductRequest : IMapFrom<UpdateProductCommand>
{
    [Required] public string Name { get; init; }

    [Required] public int CategoryId { get; set; }

    [Required] public int ManufacturerId { get; set; }

    [Required] public int UnitId { get; set; }

    [Required] public string Barcode { get; set; }

    [Required] public double Price { get; set; }

    [Required] public int CurrencyId { get; set; }
}