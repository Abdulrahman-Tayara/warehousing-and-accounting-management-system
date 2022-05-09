using Application.Common.Mappings;
using Application.Queries.Payments;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Pagination;

namespace wms.Dto.Payments;

public class GetAllPaymentsRequest : PaginationRequestParams, IMapFrom<GetAllPaymentsQuery>
{
    [FromQuery] public int InvoiceId { get; set; } = default!;
}