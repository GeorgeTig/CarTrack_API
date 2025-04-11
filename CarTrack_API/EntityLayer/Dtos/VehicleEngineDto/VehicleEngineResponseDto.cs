namespace CarTrack_API.EntityLayer.Dtos.VehicleEngineDto;

public class VehicleEngineResponseDto
{
     public int Id { get; set; }
     public string EngineType { get; set; }
     public string FuelType { get; set; }
     public string Cylinders { get; set; }
     public double Size { get; set; }
     public int HorsePower { get; set; }
     public int TorqueFtLbs { get; set; }
     public string DriveType { get; set; }
     public string Transmission { get; set; }
}