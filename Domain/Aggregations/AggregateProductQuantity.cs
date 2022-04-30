using Domain.Entities;

namespace Domain.Aggregations;

public class AggregateProductQuantity : AggregateRoot
{
    public Product Product { get; set; }
    public int QuantitySum { get; set; }

    public bool ExceedsMinLevel(int requestedQuantity) =>
        Product.HasMinLevel && (QuantitySum - requestedQuantity < Product.MinLevel);
}