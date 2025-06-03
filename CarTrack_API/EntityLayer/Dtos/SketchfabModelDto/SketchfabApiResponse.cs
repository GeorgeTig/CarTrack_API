using Newtonsoft.Json;

namespace CarTrack_API.EntityLayer.Dtos.SketchfabModelDto;

public class SketchfabApiResponse
{
    [JsonProperty("results")]
    public List<SketchfabApiModel> Results { get; set; } = new();

    [JsonProperty("total")]
    public int Total { get; set; }
}