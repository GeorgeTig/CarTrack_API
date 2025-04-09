namespace CarTrack_API.EntityLayer.Exceptions.UserExceptions;

public class UserAlreadyExistException : Exception
{
    public UserAlreadyExistException()
    {
    }
    
    public UserAlreadyExistException(string message) : base(message)
    {
    }
}