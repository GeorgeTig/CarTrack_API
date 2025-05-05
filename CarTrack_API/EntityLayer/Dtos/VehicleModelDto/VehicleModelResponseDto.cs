namespace  CarTrack_API.EntityLayer.Dtos.VehicleModelDto;

public class VehicleModelResponseDto
{
    public int Id { get; set; }
    public string ModelName { get; set; }
    public string Series { get; set; }
    public int Year { get; set; }
    public long FuelTankCapacity { get; set; }
    public long Consumption { get; set; } // The consumption is in mile/gallon
    
}