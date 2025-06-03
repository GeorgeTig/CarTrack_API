using Newtonsoft.Json;

namespace CarTrack_API.EntityLayer.Dtos.SketchfabModelDto;

public class SketchfabApiModel
{
    [JsonProperty("uid")]
    public string Uid { get; set; } = null!;

    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("viewerUrl")]
    public string ViewerUrl { get; set; } = null!;

    [JsonProperty("description")]
    public string? Description { get; set; }
}