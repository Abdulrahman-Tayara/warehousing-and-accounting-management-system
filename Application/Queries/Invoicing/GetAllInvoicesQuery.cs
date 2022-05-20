using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Invoicing;

public class GetAllInvoicesQuery : GetPaginatedQuery<Invoice>
{
    public InvoiceType? Type { get; set; }
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
        var query = _invoiceRepository.GetAll(new GetAllOptions<Invoice> {IncludeRelations = true});

        if (request.Type is not null)
        {
            query = query
                .Where(i => i.Type == request.Type);
        }
        
        return Task.FromResult(query);
    }
}