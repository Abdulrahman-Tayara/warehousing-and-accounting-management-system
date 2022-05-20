using System.ComponentModel.DataAnnotations;
using Application.Common.Mappings;
using Application.Queries.Invoicing;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using wms.Dto.Pagination;

namespace wms.Dto.Invoices;

public class InvoicesQueryParams : PaginationRequestParams, IMapFrom<GetAllInvoicesQuery>
{
    [FromQuery(Name = "type")]
    public InvoiceType? Type { get; set; } = default;
    
    [Range(0, int.MaxValue)]
    [FromQuery(Name = "account_id")]
    public int? AccountId { get; set; } = default;

    [Range(0, int.MaxValue)]
    [FromQuery(Name = "warehouse_id")]
    public int? WarehouseId { get; set; } = default;
}