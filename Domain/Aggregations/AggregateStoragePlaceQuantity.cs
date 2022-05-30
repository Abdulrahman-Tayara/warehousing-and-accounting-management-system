using Domain.Entities;

namespace Domain.Aggregations;


/// An aggregation between a storage place and a quantity of some product
/// This will most likely be used in requests like:
/// "Let me know how many of this product do I have in my inventory",
public class AggregateStoragePlaceQuantity : AggregateRoot
{
    public Product Product { get; }

    public int Quantity { get; }

    public StoragePlace StoragePlace { get; }

    public AggregateStoragePlaceQuantity(Product product, int quantity, StoragePlace storagePlace)
    {
        Product = product;
        Quantity = quantity;
        StoragePlace = storagePlace;
    }
}