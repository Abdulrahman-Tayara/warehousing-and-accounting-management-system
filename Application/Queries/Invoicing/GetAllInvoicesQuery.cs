using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Invoicing;

public class GetAllInvoicesQuery : GetPaginatedQuery<Invoice>
{
    public InvoiceType? Type { get; set; }
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
                    .GetAll(new GetAllOptions<Invoice> {IncludeRelations = true}),
                request
            )
        );
    }

    private IQueryable<Invoice> _applyFilters(IQueryable<Invoice> query, GetAllInvoicesQuery request)
    {
        if (request.Type is not null)
            query = query.Where(invoice => invoice.Type == request.Type);
        if (request.WarehouseId is not null)
            query = query.Where(invoice => invoice.WarehouseId == request.WarehouseId);
        if (request.AccountId is not null)
            query = query.Where(invoice => invoice.AccountId == request.AccountId);

        return query;
    }
}