using Application.Commands.Manufacturers.CreateManufacturer;
using Application.Common.Mappings;

namespace wms.Dto.Manufacturers;

public class CreateManufacturerRequest : IMapFrom<CreateManufacturerCommand>
{
    public string Name { get; set; }
    
    public string? Code { get; set; }
}