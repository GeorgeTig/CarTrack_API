﻿using System.Globalization;
using CarTrack_API.BusinessLogic.Mapping;
using CarTrack_API.BusinessLogic.Services.VehicleModelService;
using CarTrack_API.DataAccess.Repositories.VehicleRepository;
using CarTrack_API.EntityLayer.Dtos.VinDto.VinDecodedDto;
using CarTrack_API.EntityLayer.Dtos.VinDto.VinDeserializedDto;
using CarTrack_API.EntityLayer.Exceptions.VehicleException;
using Newtonsoft.Json;

namespace CarTrack_API.BusinessLogic.Services.VinDecoderService;


   public class VinDecoderService : IVinDecoderService
    {
        private readonly HttpClient _httpClient;
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IVehicleRepository _vehicleRepository;

        public VinDecoderService(
            HttpClient httpClient,
            IVehicleModelService vehicleModelService,
            IVehicleRepository vehicleRepository)
        {
            _httpClient = httpClient;
            _vehicleModelService = vehicleModelService;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<List<VinDecodedResponseDto>> DecodeVinAsync(string vin, int userId)
        {
            // Pasul 1: Verifică dacă VIN-ul există DEJA pentru acest utilizator.
            var existingVehicle = await _vehicleRepository.GetVehicleByVinForUserAsync(vin, userId);

            if (existingVehicle != null)
            {
                // Dacă vehiculul există, aruncăm o excepție specifică.
                // Middleware-ul o va prinde și o va transforma într-un răspuns HTTP 409 Conflict.
                throw new VehicleAlreadyExistException($"Vehicle with VIN '{vin}' already exists in your account.");
            }

            // Pasul 2: Dacă nu există, continuăm cu decodarea de la API-ul extern.
            var url = $"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVinValuesExtended/{vin}?format=json";
            var response = await _httpClient.GetStringAsync(url);

            var vehicleData = JsonConvert.DeserializeObject<RootObject>(response);
            if (vehicleData?.Results == null || !vehicleData.Results.Any())
            {
                // Poți crea o excepție custom și pentru acest caz, ex: VinNotFoundInExternalApiException
                throw new Exception("Vehicle data not found for the provided VIN from the external API.");
            }

            // Pasul 3: Normalizează, procesează și returnează datele.
            var vehicleInfo = Normalize(vehicleData.Results[0]);
            var vehicleModels = await _vehicleModelService.GetAllByVinDtoAsync(vehicleInfo);

            return MappingVehicleModel.ToVinDecodedResponnseDto(vehicleModels);
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
            dto.Year = dto.Year.Trim();

            return dto;
        }

        private static string NormalizeTransmission(string? transmission, string? transmissionSpeeds)
        {
            if (string.IsNullOrWhiteSpace(transmission) && string.IsNullOrWhiteSpace(transmissionSpeeds))
                return "";

            string style = transmission?.Trim().ToLowerInvariant() ?? "";
            string speeds = transmissionSpeeds?.Trim();

            if (style.Contains("direct drive") || style.Contains("continuously variable"))
            {
                return style;
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

            string placementPrefix = "I";

            if (!string.IsNullOrWhiteSpace(cylinderPlacement))
            {
                var placement = cylinderPlacement.Trim().ToUpperInvariant();

                if (placement.Contains("V")) placementPrefix = "V";
                else if (placement.Contains("INLINE")) placementPrefix = "I";
                else if (placement.Contains("W")) placementPrefix = "W";
                else if (placement.Contains("HORIZONTALLY") || placement.Contains("BOXER") || placement.Contains("FLAT")) placementPrefix = "H";
                else if (placement.Contains("ROTARY")) placementPrefix = "R";
                else if (placement.Contains("RADIAL")) placementPrefix = "Radial";
                else placementPrefix = "I";
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
