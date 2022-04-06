using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Common;

namespace wms.Dto.Manufacturers;

public class ManufacturerViewModel : IMapFrom<Manufacturer>, IViewModel
{
    public string Name { get; set; }
    
    public string? Code { get; set; }
}