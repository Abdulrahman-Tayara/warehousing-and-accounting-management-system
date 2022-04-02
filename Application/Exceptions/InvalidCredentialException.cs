namespace Application.Exceptions;

public class InvalidCredentialException : BaseException
{
    public InvalidCredentialException() 
        : base("Invalid username or password", StatusCodes.InvalidCredentialExceptionCode)
    {
    }
}