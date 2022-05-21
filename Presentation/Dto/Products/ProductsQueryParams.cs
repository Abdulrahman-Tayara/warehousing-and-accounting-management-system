using Application.Common.Mappings;
using Application.Queries.Products;
using Application.Queries.StoragePlaces;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Pagination;

namespace wms.Dto.Products;

public class ProductsQueryParams : PaginationRequestParams, IMapFrom<GetAllProductsQuery>
{
    [FromQuery(Name = "name")]
    public string? Name { get; set; }
}