using Application.Queries.Common;
using Application.Repositories;
using Domain.Entities;

namespace Application.Queries.Payments;

public class GetAllPaymentsQuery : GetPaginatedQuery<Payment>
{
    public int InvoiceId { get; set; }
}

public class GetAllPaymentsQueryHandler : PaginatedQueryHandler<GetAllPaymentsQuery, Payment>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetAllPaymentsQueryHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    protected override Task<IQueryable<Payment>> GetQuery(GetAllPaymentsQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            _paymentRepository
                .GetAll(new GetAllOptions<Payment> {IncludeRelations = true})
                .Where(payment => payment.InvoiceId == request.InvoiceId || request.InvoiceId == default)
        );
    }
}