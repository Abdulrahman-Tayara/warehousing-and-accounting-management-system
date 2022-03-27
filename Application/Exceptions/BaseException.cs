namespace Application.Exceptions;

public class BaseException : Exception
{
    public int Code { get; set; }

    public BaseException()
    {
    }
    
    public BaseException(int code = StatusCodes.UnknownExceptionCode)
    {
        Code = code;
    }

    public BaseException(string? message = null, int code = StatusCodes.UnknownExceptionCode) : base(message)
    {
        Code = code;
    }
}