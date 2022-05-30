namespace Domain.Entities;

public class BaseEntity<TKey> : IEntity
{
    public TKey Id { get; set; }
}

public interface IEntity {}