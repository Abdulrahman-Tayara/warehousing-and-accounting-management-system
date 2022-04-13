using System.ComponentModel.DataAnnotations;
using Application.Commands.Warehouses;
using Application.Common.Mappings;

namespace wms.Dto.Warehouses;

public class CreateWarehouseRequest : IMapFrom<CreateWarehouseCommand>
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Location { get; set; }
}

public class UpdateWarehouseRequest : IMapFrom<UpdateWarehouseCommand>
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Location { get; set; }
}