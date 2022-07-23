using Domain.Exceptions;

namespace Application.Exceptions;

public class ForbiddenAccessException : BaseException
{
    public ForbiddenAccessException() : base("Forbidden Access", StatusCodes.ForbiddenAccessExceptionCode)
    {
    }
}