using Domain.Aggregations;

namespace Domain.Entities;

public class AggregateProductQuantity : AggregateRoot
{
    public Product Product { get; set; }
    public int QuantitySum { get; set; }

    public bool ExceedsMinLevel(int requestedQuantity) => QuantitySum - requestedQuantity < Product.MinLevel;
}