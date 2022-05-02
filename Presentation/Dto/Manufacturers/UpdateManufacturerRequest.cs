using Application.Commands.Manufacturers;
using Application.Common.Mappings;

namespace wms.Dto.Manufacturers;

public class UpdateManufacturerRequest : IMapFrom<UpdateManufacturerCommand>
{
    public string Name { get; set; }
    
    public string? Code { get; set; }
}