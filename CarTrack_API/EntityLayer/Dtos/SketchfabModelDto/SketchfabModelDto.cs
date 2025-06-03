namespace CarTrack_API.EntityLayer.Dtos.SketchfabModelDto;

public class SketchfabModelDto
{
    public string Uid { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ViewerUrl { get; set; } = null!;
    public string? Description { get; set; }
}