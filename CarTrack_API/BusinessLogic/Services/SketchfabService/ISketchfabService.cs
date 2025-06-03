using CarTrack_API.EntityLayer.Dtos.SketchfabModelDto;

namespace CarTrack_API.BusinessLogic.Services.SketchfabService;

public interface ISketchfabService
{
    Task<List<SketchfabModelDto>> SearchModelsAsync(string query);
}