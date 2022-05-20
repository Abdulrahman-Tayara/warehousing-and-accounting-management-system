using Domain.Entities;

namespace Domain.Aggregations;

public class AggregateProductQuantity : AggregateRoot
{
    public Product? Product { get; set; }
    public int InputQuantities { get; set; }
    public int OutputQuantities { get; set; }

    public int QuantitySum => InputQuantities - OutputQuantities;

    public bool ExceedsMinLevel(int requestedQuantity) =>
        Product!.HasMinLevel && (QuantitySum - requestedQuantity < Product.MinLevel);
}