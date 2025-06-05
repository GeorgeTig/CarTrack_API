using System.Text.Json.Serialization;

namespace CarTrack_API.EntityLayer.Dtos.Maintenance;

    public class MaintenanceItemInputDto
    {
        [JsonPropertyName("typeId")] // Se asigură că se mapează din JSON "typeId"
        public int TypeId { get; set; }

        [JsonPropertyName("name")]   // Se asigură că se mapează din JSON "name"
        public string Name { get; set; }
    }