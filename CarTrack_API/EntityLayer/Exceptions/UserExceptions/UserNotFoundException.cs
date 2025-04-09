namespace CarTrack_API.EntityLayer.Exceptions.UserExceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException()
    {
    }
    
    public UserNotFoundException(string message) : base(message)
    {
    }
}