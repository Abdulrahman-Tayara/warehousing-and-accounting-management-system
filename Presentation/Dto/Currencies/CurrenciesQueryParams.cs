using Application.Common.Mappings;
using Application.Queries.Currencies;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Pagination;

namespace wms.Dto.Currencies;

public class CurrenciesQueryParams : PaginationRequestParams, IMapFrom<GetAllCurrenciesQuery>
{
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
}