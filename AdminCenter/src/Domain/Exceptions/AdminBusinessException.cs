namespace AdminCenter.Domain.Exceptions;
public class AdminBusinessException : Exception
{
    public AdminBusinessException()
    {
    }

    public AdminBusinessException(string? message) : base(message)
    {
    }

    public AdminBusinessException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
