using Application.Common.Mappings;
using Application.Queries.Categories;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Pagination;

namespace wms.Dto.Categories;

public class CategoriesQueryParams : PaginationRequestParams, IMapFrom<GetAllCategoriesQuery>
{
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
}