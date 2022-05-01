using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Invoicing;

public class GetAllInvoicesQuery : GetPaginatedQuery<Invoice>
{
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
            _invoiceRepository.GetAll(new GetAllOptions<Invoice> {IncludeRelations = true}));
    }
}