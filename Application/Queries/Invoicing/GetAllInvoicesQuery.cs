using Application.Common.Security;
using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Invoicing;

[Authorize(Method = Method.Read, Resource = Resource.Invoices)]
public class GetAllInvoicesQuery : GetPaginatedQuery<Invoice>
{
    public InvoiceType? Type { get; set; }
    public InvoiceAccountType? AccountType { get; set; }
    public int? AccountId { get; set; } = default;
    public int? WarehouseId { get; set; } = default;
}

public class GetAllInvoicesQueryHandler : PaginatedQueryHandler<GetAllInvoicesQuery, Invoice>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public GetAllInvoicesQueryHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    protected override Task<IQueryable<Invoice>> GetQuery(GetAllInvoicesQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            _applyFilters(
                _invoiceRepository
                    .GetAll(new GetAllOptions<Invoice> {IncludeRelations = true})
                    .OrderByDescending(invoice => invoice.CreatedAt),
                request
            )
        );
    }

    private IQueryable<Invoice> _applyFilters(IQueryable<Invoice> query, GetAllInvoicesQuery request)
    {
        if (request.Type is not null)
            query = query.Where(invoice => invoice.Type == request.Type);
        if (request.AccountType is not null)
            query = query.Where(invoice => invoice.AccountType == request.AccountType);
        if (request.WarehouseId is not null)
            query = query.Where(invoice => invoice.WarehouseId == request.WarehouseId);
        if (request.AccountId is not null)
            query = query.Where(invoice => invoice.AccountId == request.AccountId);

        return query;
    }
}