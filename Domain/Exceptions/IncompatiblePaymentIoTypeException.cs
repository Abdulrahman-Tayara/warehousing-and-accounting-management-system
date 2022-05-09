namespace Domain.Exceptions;

public class IncompatiblePaymentIoTypeException : BaseException
{
    public IncompatiblePaymentIoTypeException(string? message = "Incompatible payment IO type with invoice IO type",
        int code = StatusCodes.IncompatiblePaymentIoTypeExceptionCode) : base(message, code)
    {
    }
}