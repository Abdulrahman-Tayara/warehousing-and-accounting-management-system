namespace Domain.Exceptions;

public static class StatusCodes
{
    public const int UnknownExceptionCode = 500;
    public const int ProductMinLevelExceededExceptionCode = 432;
    public const int OverPayedException = 433;
    public const int IncompatiblePaymentIoTypeExceptionCode = 434;
    public const int InvoiceClosedException = 435;
    public const int InvoiceOpenedException = 435;
}