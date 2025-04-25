namespace CarTrack_API.EntityLayer.Exceptions.VehicleException;

public class VehicleAlreadyExistException : Exception
{
    public VehicleAlreadyExistException()
    {
    }
    
    public VehicleAlreadyExistException(string message) : base(message)
    {
    }
}