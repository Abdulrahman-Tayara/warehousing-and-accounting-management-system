using Application.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Unit = MediatR.Unit;

namespace Application.Queries.Payments;

public class CheckInvoicePaymentQuery : IRequest
{
    public int InvoiceId { get; set; }

    public double Amount { get; set; }
}

public class CheckInvoicePaymentQueryHandler : IRequestHandler<CheckInvoicePaymentQuery>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IInvoiceRepository _invoiceRepository;

    public CheckInvoicePaymentQueryHandler(IPaymentRepository paymentRepository, IInvoiceRepository invoiceRepository)
    {
        _paymentRepository = paymentRepository;
        _invoiceRepository = invoiceRepository;
    }


    public async Task<Unit> Handle(CheckInvoicePaymentQuery request, CancellationToken cancellationToken)
    {
        double paymentsSum = _paymentRepository.GetAll()
            .Where(payment => payment.Id == request.InvoiceId)
            .Sum(payment => payment.Amount);
        
        Invoice invoice = await _invoiceRepository.FindByIdAsync(request.InvoiceId);

        if (request.Amount + paymentsSum > invoice.TotalPrice)
        {
            throw new OverPayedException();
        }

        return Unit.Value;
    }
}