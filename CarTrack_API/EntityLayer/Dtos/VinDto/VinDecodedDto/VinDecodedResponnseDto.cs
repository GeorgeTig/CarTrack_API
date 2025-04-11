namespace CarTrack_API.EntityLayer.Dtos.VinDto.VinDecodedDto;

public class VinDecodedResponnseDto
{
    public string SeriesName { get; set; }
    public string Producer { get; set; }

    public List<ModelDecodedDto> VehicleModelInfo { get; set; }
}

public class ModelDecodedDto
{
    public int Year { get; set; }
    public int ModelId { get; set; }
    public List<EngineDecodedDto> EngineInfo { get; set; }
    public List<BodyDecodedDto> BodyInfo { get; set; }
}

public class EngineDecodedDto
{
    public int EngineId { get; set; }
    public required string EngineType { get; set; }
    public required string DriveType { get; set; }
    public double Size { get; set; }
    public int Horsepower { get; set; }
    public required string Transmission { get; set; }
}

public class BodyDecodedDto
{
    public int BodyId { get; set; }
    public required string BodyType { get; set; }
    public int DoorNumber { get; set; }
    public int SeatNumber { get; set; }
}