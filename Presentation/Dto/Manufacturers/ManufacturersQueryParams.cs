using Application.Common.Mappings;
using Application.Queries.Manufacturers;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Pagination;

namespace wms.Dto.Manufacturers;

public class ManufacturersQueryParams : PaginationRequestParams, IMapFrom<GetAllManufacturersQuery>
{
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
}