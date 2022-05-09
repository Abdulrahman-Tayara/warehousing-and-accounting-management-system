namespace Domain.Exceptions;

public class InvoiceClosedException : BaseException
{
    public InvoiceClosedException(string? message = "Invoice is closed", int code = StatusCodes.InvoiceClosedException)
        : base(message, code)
    {
    }
}