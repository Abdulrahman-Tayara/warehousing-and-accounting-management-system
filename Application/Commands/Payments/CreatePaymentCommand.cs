using Application.Common.Dtos;
using Application.Queries.Payments;
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

    public DateTime CreatedAt { get; set; }
}

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, int>
{
    private readonly Lazy<IUnitOfWork> _unitOfWork;
    private readonly Mediator _mediator;

    public CreatePaymentCommandHandler(Lazy<IUnitOfWork> unitOfWork, Mediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var checkInvoicePaymentQuery = new CheckInvoicePaymentQuery
            {InvoiceId = request.InvoiceId, Amount = request.Amount};
        await _mediator.Send(checkInvoicePaymentQuery, cancellationToken);

        using var unitOfWork = _unitOfWork.Value;
        Payment payment = new Payment
        {
            InvoiceId = request.InvoiceId,
            Note = request.Note,
            PaymentType = request.PaymentType,
            PaymentIoType = request.PaymentIoType,
            Amount = request.Amount,
            CurrencyId = request.CurrencyId,
            CreatedAt = DateTime.Now
        };

        var savePaymentAction = await unitOfWork.PaymentRepository.CreateAsync(payment);
        var savedPayment = await savePaymentAction();

        var currencyAmounts = request.CurrencyAmounts
            .Select(c => new CurrencyAmount
            {
                ObjectId = savedPayment.Id,
                Key = CurrencyAmountKey.Payment,
                Amount = c.Value,
                CurrencyId = c.CurrencyId
            });

        var saveCurrencyAmountsAction = await unitOfWork.CurrencyAmountRepository.CreateAllAsync(currencyAmounts);
        var _ = await saveCurrencyAmountsAction();

        await unitOfWork.CommitAsync();

        return payment.Id;
    }
}