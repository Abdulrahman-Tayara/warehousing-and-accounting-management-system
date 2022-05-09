using Application.Common.Dtos;
using Application.Repositories.UnitOfWork;
using Domain.Aggregations;
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

        InvoicePayments invoicePayments = await unitOfWork.InvoicePaymentsRepository.FindByInvoiceId(request.InvoiceId);

        invoicePayments.AddPayment(new Payment
        {
            InvoiceId = request.InvoiceId,
            Note = request.Note,
            PaymentType = request.PaymentType,
            PaymentIoType = request.PaymentIoType,
            Amount = request.Amount,
            CurrencyId = request.CurrencyId,
            CreatedAt = DateTime.Now
        });

        var saveAction = await unitOfWork.InvoicePaymentsRepository.CreatePayments(invoicePayments);
        var savedInvoicePayments = await saveAction();

        var addedPayments = savedInvoicePayments.Payments.Except(invoicePayments.Payments);
        var addedPayment = addedPayments.First();

        var currencyAmounts = request.CurrencyAmounts
            .Select(c => new CurrencyAmount
            {
                ObjectId = addedPayment.Id,
                Key = CurrencyAmountKey.Payment,
                Amount = c.Value,
                CurrencyId = c.CurrencyId
            });

        var saveCurrencyAmountsAction = await unitOfWork.CurrencyAmountRepository.CreateAllAsync(currencyAmounts);
        var _ = await saveCurrencyAmountsAction();

        await unitOfWork.CommitAsync();

        return addedPayment.Id;
    }
}