using System.Globalization;
using System.Xml;
using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.BusinessLogic.Services.VehicleModelService;
using CarTrack_API.EntityLayer.Dtos.VinDto.VinDecodedDto;
using CarTrack_API.EntityLayer.Dtos.VinDto.VinDeserializedDto;
using Newtonsoft.Json;

namespace CarTrack_API.BusinessLogic.Services.VinDecoderService;

public class VinDecoderService(HttpClient httpClient, IVehicleModelService vehicleModelService ) : IVinDecoderService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IVehicleModelService _vehicleModelService = vehicleModelService;

    public async Task<List<VinDecodedResponnseDto>> DecodeVinAsync(string vin)
    {
        
        
        var url = $"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVinValuesExtended/{vin}?format=json"; //NHTSA API
        var response = await _httpClient.GetStringAsync(url);
        
        var vehicle = JsonConvert.DeserializeObject<RootObject>(response);
        if (vehicle == null)
        {
            throw new Exception("VehicleModel not found");
        }
        // normalize the vehicleInfo

        var vehicleInfo = vehicle.Results[0];
        vehicleInfo = Normalize(vehicleInfo);

        var vehicleModels =await _vehicleModelService.GetAllByVinDtoAsync(vehicleInfo);
        
        var vehicleModelsResponse = MappingVehicleModel.ToVinDecodedResponnseDto(vehicleModels);

        return vehicleModelsResponse;

    }

    public static VehicleVinDeserializedDto Normalize(VehicleVinDeserializedDto dto)
    {
        dto.FuelTypePrimary = NormalizeFuelType(dto.FuelTypePrimary);
        dto.Body = NormalizeBody(dto.Body);
        dto.HP = NormalizeString(dto.HP);
        dto.Producer = NormalizeString(dto.Producer);
        dto.Series = NormalizeString(dto.Series);
        dto.Cylinders = NormalizeCylinders(dto.Cylinders, dto.CylinderPlacement);
        dto.Transmission = NormalizeTransmission(dto.Transmission, dto.TransmissionSpeeds);
        dto.Year = dto.Year?.Trim();

        return dto;
    }

    private static string NormalizeTransmission(string? transmission, string? transmissionSpeeds)
    {
        if (string.IsNullOrWhiteSpace(transmission) && string.IsNullOrWhiteSpace(transmissionSpeeds))
            return "";

        string style = transmission?.Trim().ToLowerInvariant() ?? "";
        string speeds = transmissionSpeeds?.Trim();

        // Tratează cazuri speciale ca "direct drive" sau "continuously variable"
        if (style.Contains("direct drive") || style.Contains("continuously variable"))
        {
            return style; // ex: "1-speed direct drive" sau "continuously variable-speed automatic"
        }

        if (!string.IsNullOrWhiteSpace(speeds))
        {
            return $"{speeds}-speed {style}".Trim();
        }

        return style;
    }
    
    private static string NormalizeCylinders(string? cylinders, string? cylinderPlacement)
    {
        if (string.IsNullOrWhiteSpace(cylinders))
            return "";

        string placementPrefix = "I"; // Default fallback

        if (!string.IsNullOrWhiteSpace(cylinderPlacement))
        {
            var placement = cylinderPlacement.Trim().ToUpperInvariant();

            if (placement.Contains("V"))
                placementPrefix = "V";
            else if (placement.Contains("INLINE"))
                placementPrefix = "I";
            else if (placement.Contains("W"))
                placementPrefix = "W";
            else if (placement.Contains("HORIZONTALLY") || placement.Contains("BOXER") || placement.Contains("FLAT"))
                placementPrefix = "H";
            else if (placement.Contains("ROTARY"))
                placementPrefix = "R";
            else if (placement.Contains("RADIAL"))
                placementPrefix = "Radial"; // Rare, dar dacă vrei să le păstrezi diferit
            else
                placementPrefix = "I"; // fallback
        }

        return $"{placementPrefix}{cylinders.Trim()}";
    }

    private static string NormalizeFuelType(string? fuel)
    {
        if (fuel == null) return "";

        return fuel.ToLower() switch
        {
            "gasoline" => "gas",
            "diesel" => "diesel",
            "electric" => "electric",
            "fuel cell" or "fuel cell electric" => "electric (fuel cell)",
            "flex-fuel" or "ethanol" => "flex-fuel (FFV)",
            _ => fuel.ToLower()
        };
    }

    private static string NormalizeBody(string? body)
    {
        if (string.IsNullOrWhiteSpace(body)) return "";

        body = body.ToLower();
        if (body.Contains("truck")) return "Truck";
        if (body.Contains("suv")) return "SUV";
        if (body.Contains("sedan")) return "Sedan";
        if (body.Contains("coupe")) return "Coupe";
        if (body.Contains("convertible")) return "Convertible";
        if (body.Contains("wagon")) return "Wagon";
        if (body.Contains("hatchback")) return "Hatchback";
        if (body.Contains("minivan")) return "Minivan";
        if (body.Contains("cargo")) return "Cargo Van";
        if (body.Contains("passenger")) return "Passenger Van";
        if (body.Contains("ext van")) return "Ext Van";
        if (body.Contains("van")) return "Van";

        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(body);
    }
    
    private static string NormalizeString(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? "" : value.Trim();
    }
}