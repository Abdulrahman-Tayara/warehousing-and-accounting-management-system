using System.ComponentModel.DataAnnotations;
using Application.Commands.StoragePlaces;
using Application.Common.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace wms.Dto.StoragePlaces;

public class CreateStoragePlaceRequest : IMapFrom<CreateStoragePlaceCommand>
{
    [FromRoute(Name = "warehouseId")]
    public int WarehouseId { get; set; }
    
    public string Name { get; set; }

    public int? ContainerId { get; set; }
}

public class UpdateStoragePlaceRequest : IMapFrom<UpdateStoragePlaceCommand>
{
    [FromRoute(Name = "id")]
    public int Id { get; set; }
    
    [FromRoute(Name = "warehouseId")]
    [Required]
    public int WarehouseId { get; set; }
    
    [Required]
    public string Name { get; set; }

    public int? ContainerId { get; set; }
}