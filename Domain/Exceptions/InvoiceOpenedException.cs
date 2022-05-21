namespace Domain.Exceptions;

public class InvoiceOpenedException : BaseException
{
    public InvoiceOpenedException(string? message = "Invoice is closed", int code = StatusCodes.InvoiceOpenedException)
        : base(message, code)
    {
    }
}