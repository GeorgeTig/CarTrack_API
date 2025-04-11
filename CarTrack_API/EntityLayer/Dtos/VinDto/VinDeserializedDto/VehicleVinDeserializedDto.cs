using Newtonsoft.Json;

namespace CarTrack_API.EntityLayer.Dtos.VinDto.VinDeserializedDto;

public class VehicleVinDeserializedDto
{
    [JsonProperty("Make")] // Producer in your DTO corresponds to the JSON "Make"
    public string Producer { get; set; }
    
    [JsonProperty("Model")]
    public string Series { get; set; }
    
    [JsonProperty("ModelYear")] // If the JSON field is "ModelYear" instead of "Year"
    public string Year { get; set; }
    
    [JsonProperty("BodyClass")] // Assuming "BodyClass" is used in the JSON
    public string Body { get; set; }
    
    [JsonProperty("EngineConfiguration")] // Adjust this if the JSON returns something else
    public string CylinderPlacement { get; set; }
    
    [JsonProperty("FuelTypePrimary")]
    public string FuelTypePrimary { get; set; }
    
    // If the JSON returns horsepower under a different key (for example, "EngineHP")
    [JsonProperty("EngineHP")]
    public string HP { get; set; }
    
    [JsonProperty("EngineCylinders")] // Assuming "VehicleType" is used in the JSON
    public string Cylinders { get; set; }
    
    [JsonProperty("Doors")]
    public string Doors { get; set; }
    
    [JsonProperty("TransmissionStyle")]
    public string Transmission { get; set; }
    
    [JsonProperty("TransmissionSpeeds")]
    public string TransmissionSpeeds { get; set; }
}

public class RootObject
{
    public int Count { get; set; }
    public string Message { get; set; }
    public string SearchCriteria { get; set; }
    public List<VehicleVinDeserializedDto> Results { get; set; }
}

