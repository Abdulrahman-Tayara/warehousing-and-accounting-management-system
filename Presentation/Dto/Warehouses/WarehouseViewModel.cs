using Application.Common.Mappings;
using Domain.Entities;
using wms.Dto.Common;

namespace wms.Dto.Warehouses;

public class WarehouseViewModel : IViewModel, IMapFrom<Warehouse>
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Location { get; set; }
}