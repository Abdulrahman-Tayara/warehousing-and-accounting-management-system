using Application.Common.Mappings;
using Application.Queries.Warehouses;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Pagination;

namespace wms.Dto.Warehouses;

public class WarehousesQueryParams : PaginationRequestParams, IMapFrom<GetAllWarehousesQuery>
{
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
}