namespace CarTrack_API.EntityLayer.Exceptions.UserRoleExceptions;

public class UserRoleNotFoundException : Exception
{
    public UserRoleNotFoundException()
    {
    }
    
    public UserRoleNotFoundException(string message) : base(message)
    {
    }
}