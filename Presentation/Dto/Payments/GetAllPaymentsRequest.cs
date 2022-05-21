using Application.Common.Mappings;
using Application.Queries.Payments;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Pagination;

namespace wms.Dto.Payments;

public class GetAllPaymentsRequest : PaginationRequestParams, IMapFrom<GetAllPaymentsQuery>
{
    [FromQuery] public int InvoiceId { get; set; } = default;

    [FromQuery] public PaymentType? PaymentType { get; set; } = default;

    [FromQuery] public PaymentIoType? PaymentIoType { get; set; } = default;
}