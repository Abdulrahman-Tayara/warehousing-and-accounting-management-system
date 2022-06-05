using System.Diagnostics;
using Application.Commands.Notifications;
using Application.Common.Dtos;
using Application.Queries.Invoicing;
using Application.Queries.Notifications;
using Domain.Entities;
using MediatR;

namespace Application.EventNotifications.Invoices.InvoiceCreated;

public class MinLevelNotificationHandler : INotificationHandler<InvoiceCreatedNotification>
{
    private readonly IMediator _mediator;

    public MinLevelNotificationHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(InvoiceCreatedNotification notification, CancellationToken cancellationToken)
    {
        try
        {
            Task handleTask = notification.Invoice.Type == InvoiceType.Out
                ? HandleOutInvoice(notification.Invoice)
                : HandleInInvoice(notification.Invoice);

            await handleTask;
        }
        catch (Exception e)
        {
            Debug.Fail(e.ToString());   
        }
    }

    private async Task HandleOutInvoice(Invoice invoice)
    {
        var query = new GetProductsWithNewMinLevelWarningsQuery(invoice.Id);
        var productsWithNewMinLevelWarnings = await _mediator.Send(query);

        IList<NotificationDto> notificationDtos = productsWithNewMinLevelWarnings
            .Select(product => new NotificationDto(product.Id, NotificationType.MinLevelExceeded))
            .ToList();

        var command = new CreateNotificationsCommand(notificationDtos);
        var createdNotificationIds = await _mediator.Send(command);
    }

    private async Task HandleInInvoice(Invoice invoice)
    {
        var productsQuery = new GetProductsWithNewMinLevelResolvesQuery(invoice.Id);
        var productsWithNewMinLevelResolves = (await _mediator.Send(productsQuery)).ToList();

        var notificationQuery = new GetAllNotificationsQuery
        {
            Page = 1,
            PageSize = int.MaxValue,
            ObjectIds = productsWithNewMinLevelResolves.Select(product => product.Id),
            NotificationType = NotificationType.MinLevelExceeded,
            IsValid = true
        };
        var notificationsPage = await _mediator.Send(notificationQuery);
        var notifications = notificationsPage.ToList();

        var notificationsResolved = notifications
            .Where(notificationResolved =>
                productsWithNewMinLevelResolves.Any(product => product.Id == notificationResolved.ObjectId))
            .ToList();

        notificationsResolved
            .ForEach(notification => notification.IsValid = false);

        var command = new UpdateNotificationsCommand(notificationsResolved);
        var updatedNotificationIds = await _mediator.Send(command);
    }
}