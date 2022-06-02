using Application.Common.Events;
using Application.Services.Events;
using Domain.Events;
using MediatR;

namespace Infrastructure.Services;

public class EventPublisherService : IEventPublisherService
{

    private readonly IMediator _mediator;
    
    public EventPublisherService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Publish(DomainEvent domainEvent)
    {
        return _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
    }

    private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
    {
        return (INotification)Activator.CreateInstance(
            typeof(DomainNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;
    }
}