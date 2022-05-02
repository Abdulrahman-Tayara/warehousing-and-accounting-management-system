using Microsoft.AspNetCore.Mvc;
using wms.Dto.Pagination;

namespace wms.Dto.StoragePlaces;

public class StoragePlacesRequestParams : PaginationRequestParams
{
    [FromQuery(Name = "is_parent")]
    public bool? IsParent { get; set; }
    [FromQuery(Name = "container_id")]
    public int? ContainerId { get; set; }
}