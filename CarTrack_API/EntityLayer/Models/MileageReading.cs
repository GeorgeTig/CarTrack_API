namespace CarTrack_API.EntityLayer.Models;

public class MileageReading
{
    public int Id { get; set; }
    
    // Când s-a făcut citirea
    public DateTime ReadingDate { get; set; } 
    
    // Valoarea absolută a odometrului la acel moment
    public double OdometerValue { get; set; } 
    
    // Sursa citirii (foarte util pentru viitor)
    public string Source { get; set; } // ex: "QuickSync", "MaintenanceLog", "FuelLog", "AutomaticOBD"

    // Relația cu vehiculul
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
}