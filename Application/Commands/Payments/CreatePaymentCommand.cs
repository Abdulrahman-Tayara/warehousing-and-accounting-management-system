using Application.Common.Dtos;
using Application.EventNotifications.Payments.PaymentCreated;
using Application.Repositories.UnitOfWork;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Payments;

public class CreatePaymentCommand : IRequest<int>
{
    public int InvoiceId { get; set; }

    public string? Note { get; set; }

    public double Amount { get; set; }

    public int CurrencyId { get; set; }

    public PaymentType PaymentType { get; set; }

    public PaymentIoType PaymentIoType { get; set; }

    public IEnumerable<CurrencyAmountDto> CurrencyAmounts { get; set; }
}

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, int>
{
    private readonly Lazy<IUnitOfWork> _unitOfWork;

    private readonly IMediator _mediator;

    public CreatePaymentCommandHandler(Lazy<IUnitOfWork> unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        using var unitOfWork = _unitOfWork.Value;

        var invoicePayments = await unitOfWork.InvoicePaymentsRepository.FindByInvoiceId(request.InvoiceId);

        invoicePayments.AddPayment(new Payment
        {
            InvoiceId = request.InvoiceId,
            Note = request.Note,
            PaymentType = request.PaymentType,
            PaymentIoType = request.PaymentIoType,
            Amount = request.Amount,
            CurrencyId = request.CurrencyId,
            CreatedAt = DateTime.Now,
            CurrencyAmounts = request.CurrencyAmounts
                .Select(c => new CurrencyAmount
                {
                    Key = CurrencyAmountKey.Payment,
                    Amount = c.Value,
                    CurrencyId = c.CurrencyId
                })
        });

        var saveAction = await unitOfWork.InvoicePaymentsRepository.Save(invoicePayments);
        var savedInvoicePayments = await saveAction();

        var addedPayments = savedInvoicePayments.Payments.Except(invoicePayments.Payments);
        var addedPayment = addedPayments.First();

        await _mediator.Publish(new PaymentCreatedNotification(addedPayment), cancellationToken);
        
        await unitOfWork.CommitAsync();

        return addedPayment.Id;
    }
}