using Application.Common.Mappings;
using Application.Queries.Users;
using Application.Queries.Warehouses;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Pagination;

namespace wms.Dto.Users;

public class UsersQueryParams : PaginationRequestParams, IMapFrom<GetAllUsersQuery>
{
    [FromQuery(Name = "username")]
    public string? UserName { get; set; }
}