namespace Domain.Events;

public class ProductMinLevelUpdated : DomainEvent
{
    public int ProductId { get; }
    
    public int MinLevelBefore { get; }
    
    public int MinLevelAfter { get; }

    public ProductMinLevelUpdated(int productId, int minLevelBefore, int minLevelAfter)
    {
        ProductId = productId;
        MinLevelBefore = minLevelBefore;
        MinLevelAfter = minLevelAfter;
    }

    public bool MinLevelIncreased() => MinLevelAfter > MinLevelBefore;

    public bool MinLevelDecreased() => MinLevelAfter < MinLevelBefore;
}