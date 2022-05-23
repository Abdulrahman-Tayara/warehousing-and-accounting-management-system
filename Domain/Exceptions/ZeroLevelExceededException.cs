namespace Domain.Exceptions;

public class ZeroLevelExceededException : BaseException
{
    public IList<int> ProductsWithZeroLevelExceeded { get; }

    public ZeroLevelExceededException(int productId) : this(new List<int> {productId})
    {
    }

    public ZeroLevelExceededException(IList<int> productsWithZeroLevelExceeded,
        string? message = "Zero level exceeded") : base(message,
        StatusCodes.ZeroLevelExceededExceptionCode)
    {
        ProductsWithZeroLevelExceeded = productsWithZeroLevelExceeded;
    }
}