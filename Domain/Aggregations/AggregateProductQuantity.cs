using Domain.Entities;

namespace Domain.Aggregations;

public class AggregateProductQuantity : AggregateRoot
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int InputQuantities { get; set; }
    public int OutputQuantities { get; set; }

    public int QuantitySum => InputQuantities - OutputQuantities;

    public bool ExceedsMinLevel(int requestedQuantity) =>
        Product!.HasMinLevel && (QuantitySum - requestedQuantity < Product.MinLevel);

    public AggregateProductQuantity AddProduct(Product product)
    {
        Product = product;
        return this;
    }

    public bool ExceedsZeroLevel(int requestedQuantity) => QuantitySum - requestedQuantity < 0;
}