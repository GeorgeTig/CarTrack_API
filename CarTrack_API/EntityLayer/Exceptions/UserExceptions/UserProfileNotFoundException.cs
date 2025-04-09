namespace CarTrack_API.EntityLayer.Exceptions.UserExceptions;

public class UserProfileNotFoundException : Exception
{
    public UserProfileNotFoundException()
    {
    }
    
    public UserProfileNotFoundException(string message) : base(message)
    {
    }
}