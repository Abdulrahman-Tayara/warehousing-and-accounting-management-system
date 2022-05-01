using Domain.Exceptions;

namespace Application.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException() : this(null)
    {
    }

    public NotFoundException(string? message) : base(message, StatusCodes.NotFoundExceptionCode)
    {
    }
    
    public NotFoundException(string name, object key)
        : this($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}