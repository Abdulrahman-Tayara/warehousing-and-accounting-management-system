namespace Domain.Events;

public abstract class DomainEvent
{
    public bool IsPublished { get; set; } = false;
    public DateTime CreatedAt = DateTime.Now;
}

public interface IHasDomainEvents
{
    public IList<DomainEvent> Events { get; set; }
}