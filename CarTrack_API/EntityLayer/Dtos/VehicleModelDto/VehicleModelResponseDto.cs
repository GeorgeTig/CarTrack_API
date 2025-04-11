namespace CarTrack_API.EntityLayer.Dtos.VehicleModelDto;

public class VehicleModelResponseDto
{
    public int Id { get; set; }
    public int Year { get; set; }
    public required string SeriesName { get; set; }
    public required string ModelName { get; set; }
    public long FuelTankCapacity { get; set; }
    public long Consumption { get; set; }
    
}