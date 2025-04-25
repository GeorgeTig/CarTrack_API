namespace CarTrack_API.EntityLayer.Exceptions.VehicleException;

public class VehicleNotFoundException : Exception
{
    public VehicleNotFoundException()
    {
    }
    
    public VehicleNotFoundException(string message) : base(message)
    {
    }
}