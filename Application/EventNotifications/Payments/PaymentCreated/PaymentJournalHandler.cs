using Application.Commands.Journals;
using Application.Queries.Invoicing;
using Application.Settings;
using Domain.Entities;
using MediatR;

namespace Application.EventNotifications.Payments.PaymentCreated;

public class PaymentJournalHandler : INotificationHandler<PaymentCreatedNotification>
{
    private readonly IMediator _mediator;

    private readonly ApplicationSettings _applicationSettings;

    public PaymentJournalHandler(IMediator mediator, ApplicationSettings applicationSettings)
    {
        _mediator = mediator;
        _applicationSettings = applicationSettings;
    }

    public async Task Handle(PaymentCreatedNotification notification, CancellationToken cancellationToken)
    {
        var payment = notification.Payment;
        
        var invoice = await _mediator.Send(new GetInvoiceQuery {Id = payment.InvoiceId}, cancellationToken);

        if (payment.PaymentIoType == PaymentIoType.In)
        {
            await _handleFromCustomerToCashDrawer(invoice, payment, cancellationToken);
            await _handleFromCashDrawerToSales(payment, cancellationToken);
        }
        else if (payment.PaymentIoType == PaymentIoType.Out)
        {
            await _handleFromCashDrawerToCustomer(invoice, payment, cancellationToken);
            await _handleFromPurchasesToCashDrawer(payment, cancellationToken);
        }
    }

    private async Task _handleFromCustomerToCashDrawer(Invoice invoice, Payment payment, CancellationToken cancellationToken)
    {
        int defaultCashDrawerAccountId = _applicationSettings.DefaultMainCashDrawerAccountId;

        var createJournalsCommand =
            new CreateJournalsCommand(
                invoice.AccountId.GetValueOrDefault(),
                defaultCashDrawerAccountId,
                payment.Amount,
                payment.CurrencyId
            );

        await _mediator.Send(createJournalsCommand, cancellationToken);
    }

    private async Task _handleFromCashDrawerToSales(Payment payment, CancellationToken cancellationToken)
    {
        int defaultCashDrawerAccountId = _applicationSettings.DefaultMainCashDrawerAccountId;
        int defaultSalesAccountId = _applicationSettings.DefaultSalesAccountId;

        var createJournalsCommand =
            new CreateJournalsCommand(
                defaultCashDrawerAccountId,
                defaultSalesAccountId,
                payment.Amount,
                payment.CurrencyId
            );

        await _mediator.Send(createJournalsCommand, cancellationToken);
    }

    private async Task _handleFromCashDrawerToCustomer(Invoice invoice, Payment payment, CancellationToken cancellationToken)
    {
        int defaultCashDrawerAccountId = _applicationSettings.DefaultMainCashDrawerAccountId;

        var createJournalsCommand =
            new CreateJournalsCommand(
                defaultCashDrawerAccountId,
                invoice.AccountId.GetValueOrDefault(),
                payment.Amount,
                payment.CurrencyId
            );

        await _mediator.Send(createJournalsCommand, cancellationToken);
    }

    private async Task _handleFromPurchasesToCashDrawer(Payment payment, CancellationToken cancellationToken)
    {
        int defaultPurchasesAccountId = _applicationSettings.DefaultPurchasesAccountId;
        int defaultCashDrawerAccountId = _applicationSettings.DefaultMainCashDrawerAccountId;

        var createJournalsCommand =
            new CreateJournalsCommand(
                defaultPurchasesAccountId,
                defaultCashDrawerAccountId,
                payment.Amount,
                payment.CurrencyId
            );

        await _mediator.Send(createJournalsCommand, cancellationToken);
    }
}