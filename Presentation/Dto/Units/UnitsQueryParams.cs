using Application.Common.Mappings;
using Application.Queries.Units;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Pagination;

namespace wms.Dto.Units;

public class UnitsQueryParams : PaginationRequestParams, IMapFrom<GetAllUnitsQuery>
{
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
}