using Application.Commands.Journals;
using Application.Settings;
using Domain.Entities;
using MediatR;

namespace Application.EventNotifications.Invoices.InvoiceCreated;

public class InvoiceJournalsHandler : INotificationHandler<InvoiceCreatedNotification>
{
    private readonly IMediator _mediator;

    private readonly ApplicationSettings _applicationSettings;

    public InvoiceJournalsHandler(IMediator mediator, ApplicationSettings applicationSettings)
    {
        _mediator = mediator;
        _applicationSettings = applicationSettings;
    }

    public async Task Handle(InvoiceCreatedNotification notification, CancellationToken cancellationToken)
    {
        var invoice = notification.Invoice;

        if (invoice.Type == InvoiceType.Out)
        {
            await _handleFromCashDrawerToCustomer(invoice, cancellationToken);
            await _handleFromSalesToCashDrawer(invoice, cancellationToken);
        }
        else if (invoice.Type == InvoiceType.In)
        {
            await _handleFromCustomerToCashDrawer(invoice, cancellationToken);
            await _handleFromCashDrawerToPurchases(invoice, cancellationToken);
        }
    }

    private async Task _handleFromCashDrawerToCustomer(Invoice invoice, CancellationToken cancellationToken)
    {
        int defaultCashDrawerAccountId = _applicationSettings.DefaultMainCashDrawerAccountId;
        int defaultCurrencyId = _applicationSettings.DefaultCurrencyId;

        var createJournalsCommand =
            new CreateJournalsCommand(
                defaultCashDrawerAccountId,
                invoice.AccountId.GetValueOrDefault(),
                invoice.TotalPrice,
                invoice.CurrencyId.GetValueOrDefault(defaultCurrencyId)
            );

        await _mediator.Send(createJournalsCommand, cancellationToken);
    }

    private async Task _handleFromSalesToCashDrawer(Invoice invoice, CancellationToken cancellationToken)
    {
        int defaultCashDrawerAccountId = _applicationSettings.DefaultMainCashDrawerAccountId;
        int defaultSalesAccountId = _applicationSettings.DefaultSalesAccountId;
        int defaultCurrencyId = _applicationSettings.DefaultCurrencyId;

        var createJournalsCommand =
            new CreateJournalsCommand(
                defaultSalesAccountId,
                defaultCashDrawerAccountId,
                invoice.TotalPrice,
                invoice.CurrencyId.GetValueOrDefault(defaultCurrencyId)
            );

        await _mediator.Send(createJournalsCommand, cancellationToken);
    }

    private async Task _handleFromCustomerToCashDrawer(Invoice invoice, CancellationToken cancellationToken)
    {
        int defaultCashDrawerAccountId = _applicationSettings.DefaultMainCashDrawerAccountId;
        int defaultCurrencyId = _applicationSettings.DefaultCurrencyId;

        var createJournalsCommand =
            new CreateJournalsCommand(
                invoice.AccountId.GetValueOrDefault(),
                defaultCashDrawerAccountId,
                invoice.TotalPrice,
                invoice.CurrencyId.GetValueOrDefault(defaultCurrencyId)
            );

        await _mediator.Send(createJournalsCommand, cancellationToken);
    }

    private async Task _handleFromCashDrawerToPurchases(Invoice invoice, CancellationToken cancellationToken)
    {
        int defaultPurchasesAccountId = _applicationSettings.DefaultPurchasesAccountId;
        int defaultCashDrawerAccountId = _applicationSettings.DefaultMainCashDrawerAccountId;
        int defaultCurrencyId = _applicationSettings.DefaultCurrencyId;

        var createJournalsCommand =
            new CreateJournalsCommand(
                defaultCashDrawerAccountId,
                defaultPurchasesAccountId,
                invoice.TotalPrice,
                invoice.CurrencyId.GetValueOrDefault(defaultCurrencyId)
            );

        await _mediator.Send(createJournalsCommand, cancellationToken);
    }
}