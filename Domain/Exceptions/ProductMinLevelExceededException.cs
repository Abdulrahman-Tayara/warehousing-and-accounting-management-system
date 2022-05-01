namespace Domain.Exceptions;

public class ProductMinLevelExceededException : BaseException
{
    public IList<int> ProductsWithExceededMinLevel { get; }

    public ProductMinLevelExceededException(int productId) : this(new List<int> {productId})
    {
    }

    public ProductMinLevelExceededException(IList<int> productsWithExceededMinLevel,
        string? message = "Product minimum level exceeded") : base(message,
        StatusCodes.ProductMinLevelExceededExceptionCode)
    {
        ProductsWithExceededMinLevel = productsWithExceededMinLevel;
    }
}