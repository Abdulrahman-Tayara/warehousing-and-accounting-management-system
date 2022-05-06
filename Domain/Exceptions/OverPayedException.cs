namespace Domain.Exceptions;

public class OverPayedException : BaseException
{
    public OverPayedException(string? message = "Amount specified in the payment is more than the remaining amount",
        int code = StatusCodes.OverPayedException) : base(message, code)
    {
    }
}