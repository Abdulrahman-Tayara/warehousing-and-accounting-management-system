using System.Diagnostics;
using Application.Commands.Notifications;
using Application.Common.Dtos;
using Application.Common.Events;
using Application.Queries.Notifications;
using Application.Queries.Warehouses;
using Domain.Aggregations;
using Domain.Entities;
using MediatR;

namespace Application.EventNotifications.Products.ProductMinLevelUpdated;

public class ProductMinLevelUpdatedHandler : INotificationHandler<DomainNotification<Domain.Events.ProductMinLevelUpdated>>
{
    private readonly IMediator _mediator;

    public ProductMinLevelUpdatedHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(DomainNotification<Domain.Events.ProductMinLevelUpdated> notification,
        CancellationToken cancellationToken)
    {
        var @event = notification.DomainEvent;

        try
        {
            if (@event.MinLevelIncreased())
                await _handleIncreased(@event);

            if (@event.MinLevelDecreased())
                await _handleDecreased(@event);
        }
        catch (Exception e)
        {
            Debug.Fail(e.ToString());
        }
    }

    private async Task _handleIncreased(Domain.Events.ProductMinLevelUpdated @event)
    {
        AggregateProductQuantity aggregate = await _aggregateTask(@event.ProductId);

        // Quantity is still below the updated min level
        if (aggregate.QuantitySum > @event.MinLevelAfter)
        {
            // No need to do anything
        }
        // Quantity is now below the updated min level
        else
        {
            var notificationsWithProductId = await _getNotificationsWithProductId(@event.ProductId);

            if (notificationsWithProductId.Any())
            {
                return;
            }

            var createdNotificationDtos = new List<NotificationDto>
            {
                new(@event.ProductId, NotificationType.MinLevelExceeded)
            };
            await _mediator.Send(new CreateNotificationsCommand(createdNotificationDtos));
        }
    }

    private async Task _handleDecreased(Domain.Events.ProductMinLevelUpdated @event)
    {
        AggregateProductQuantity aggregate = await _aggregateTask(@event.ProductId);

        // Quantity is still above the updated min level
        if (aggregate.QuantitySum < @event.MinLevelAfter)
        {
            // No need to do anything
        }
        // Quantity is now above the updated min level
        else
        {
            var notificationsWithProductId = await _getNotificationsWithProductId(@event.ProductId);

            notificationsWithProductId.ForEach(notification => notification.IsValid = false);

            await _mediator.Send(new UpdateNotificationsCommand(notificationsWithProductId));
        }
    }

    private async Task<AggregateProductQuantity> _aggregateTask(int productId)
    {
        var inventoryQueryOfProduct = new InventoryWarehouseQuery
        {
            Filters = new ProductMovementFilters {ProductIds = new List<int> {productId}}
        };

        var aggregatesPage = await _mediator.Send(inventoryQueryOfProduct);
        var aggregate = aggregatesPage.ToList().First();

        return aggregate;
    }

    private async Task<List<Notification>> _getNotificationsWithProductId(int productId)
    {
        var getAllNotificationQuery = new GetAllNotificationsQuery
        {
            ObjectIds = new List<int> {productId},
            IsValid = true,
            NotificationType = NotificationType.MinLevelExceeded,
            Page = 1,
            PageSize = int.MaxValue
        };

        var notificationsPage = await _mediator.Send(getAllNotificationQuery);
        return notificationsPage.ToList();
    }
}