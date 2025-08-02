namespace MissionEngineering.Platform;

public record FlightpathDemand
{
    public int FlightpathDemandFlightpathId { get; set; }

    public int FlightpathDemandModificationId { get; set; }

    public double FlightpathDemandTime_s { get; set; }

    public double HeadingAngleDemand_deg { get; set; }

    public double TotalSpeedDemand_ms { get; set; }

    public double AltitudeDemand_m { get; set; }

    public double PitchAngleDemand_deg { get; set; }

    public double BankAngleDemand_deg { get; set; }

    public double BankAngleRateDemand_deg { get; set; }
}