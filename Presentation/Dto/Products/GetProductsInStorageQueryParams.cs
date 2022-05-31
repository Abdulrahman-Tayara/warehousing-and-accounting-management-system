using Application.Common.Mappings;
using Application.Queries.Products;
using wms.Dto.Pagination;

namespace wms.Dto.Products;

public class GetProductsInStorageQueryParams : PaginationRequestParams, IMapFrom<GetAllProductsInStoragePlaceQuery>
{
    public int StoragePlaceId { get; set; }

    public bool? IncludeStoragePlaceChildren { get; set; } = true;
}