using CarTrack_API.EntityLayer.Dtos.SketchfabModelDto;
using Newtonsoft.Json;

namespace CarTrack_API.BusinessLogic.Services.SketchfabService;

public class SketchfabService : ISketchfabService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://api.sketchfab.com/v3/models";

    public SketchfabService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<SketchfabModelDto>> SearchModelsAsync(string query)
    {
        // Construiește URL-ul cu parametrii: caută vehicule descarcabile în format glb
        var url = $"{BaseUrl}?type=models&downloadable=true&archives_format=glb&categories=vehicles&q={Uri.EscapeDataString(query)}";

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Sketchfab API error: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<SketchfabApiResponse>(json);
        if (apiResponse == null || apiResponse.Results == null)
            return new List<SketchfabModelDto>();

        // Mapare simplă către DTO
        var models = apiResponse.Results.Select(m => new SketchfabModelDto
        {
            Uid = m.Uid,
            Name = m.Name,
            ViewerUrl = m.ViewerUrl,
            Description = m.Description
        }).ToList();

        return models;
    }
}