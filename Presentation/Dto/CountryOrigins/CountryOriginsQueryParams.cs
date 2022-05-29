using Application.Common.Mappings;
using Application.Queries.CountryOrigins;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Pagination;

namespace wms.Dto.CountryOrigins;

public class CountryOriginsQueryParams : PaginationRequestParams, IMapFrom<GetAllCountryOriginsQuery>
{
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
}