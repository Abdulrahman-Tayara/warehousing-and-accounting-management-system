using Domain.Events;
using MediatR;

namespace Application.Common.Events;

public class DomainNotification<T> : INotification
    where T : DomainEvent
{
    
    public T DomainEvent { get; set; }
    
    public DomainNotification(T domainEvent)
    {
        DomainEvent = domainEvent;
    }
    
}