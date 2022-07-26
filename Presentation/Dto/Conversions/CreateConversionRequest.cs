using System.ComponentModel.DataAnnotations;
using Application.Commands.Conversions;
using Application.Common.Mappings;

namespace wms.Dto.Conversions;

public class CreateConversionRequest : IMapFrom<CreateConversionCommand>
{
    [Required]
    public int FromWarehouseId { get; set; }
    
    [Required]
    public int ToWarehouseId { get; set; }

    [Required]
    public int FromProductId { get; set; }

    [Required]
    public int ToProductId { get; set; }

    [Required]
    public int FromPlaceId { get; set; }

    [Required]
    public int ToPlaceId { get; set; }

    [Required]
    public int FromQuantity { get; set; }

    [Required]
    public int ToQuantity { get; set; }

    public string? Note { get; set; }
    
    public bool IgnoreMinLevelWarnings { get; set; } = false;
}