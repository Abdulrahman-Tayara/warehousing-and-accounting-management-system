namespace Domain.Entities;

public class BaseEntity<TKey>
{
    public TKey Id { get; set; }
}