using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Invoicing;

public class GetInvoiceQuery : IRequest<Invoice>
{
    public int Id { get; set; }
}

public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, Invoice>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public GetInvoiceQueryHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public Task<Invoice> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
    {
        return _invoiceRepository.FindByIdAsync(request.Id, new FindOptions {IncludeRelations = true});
    }
}