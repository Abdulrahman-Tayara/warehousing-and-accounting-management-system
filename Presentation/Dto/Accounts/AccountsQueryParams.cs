using Application.Common.Mappings;
using Application.Queries.Accounts;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Pagination;

namespace wms.Dto.Accounts;

public class AccountsQueryParams : PaginationRequestParams, IMapFrom<GetAllAccountsQuery>
{
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
}