using System.ComponentModel.DataAnnotations;
using Application.Commands.StoragePlaces;
using Application.Common.Mappings;

namespace wms.Dto.StoragePlaces;

public class CreateStoragePlaceRequest : IMapFrom<CreateStoragePlaceCommand>
{
    public string Name { get; set; }

    public int? ContainerId { get; set; }
    
    public string? Description { get; set; }
}

public class UpdateStoragePlaceRequest : IMapFrom<UpdateStoragePlaceCommand>
{
    [Required]
    public string Name { get; set; }

    public int? ContainerId { get; set; }
    
    public string? Description { get; set; }
}