namespace CarTrack_API.EntityLayer.Dtos.Maintenance;

    public class MaintenanceRuleDto
    {
        public string MaintenanceName { get; set; }
        public int MaintenanceTypeId { get; set; }
        public BaseIntervalDto BaseInterval { get; set; }
        public List<ModifierDto> Modifiers { get; set; }
    }

    public class BaseIntervalDto
    {
        public int Mileage { get; set; }
        public int Time { get; set; }
    }

    public class ModifierDto
    {
        public string Property { get; set; }
        public List<CaseDto> Cases { get; set; }
    }

    public class CaseDto
    {
        public string Operator { get; set; }
        public object Value { get; set; } // Folosim 'object' pentru a putea avea string-uri sau numere
        public double Factor { get; set; }
    }
