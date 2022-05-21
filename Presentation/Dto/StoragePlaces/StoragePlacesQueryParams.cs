using Application.Common.Mappings;
using Application.Queries.StoragePlaces;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Pagination;

namespace wms.Dto.StoragePlaces;

public class StoragePlacesQueryParams : PaginationRequestParams, IMapFrom<GetAllStoragePlacesQuery>
{
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
    
    [FromQuery(Name = "is_parent")]
    public bool? IsParent { get; set; }
    
    [FromQuery(Name = "container_id")]
    public int? ContainerId { get; set; }
}