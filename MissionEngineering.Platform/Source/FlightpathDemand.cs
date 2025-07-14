namespace MissionEngineering.Platform;

public record FlightpathDemand
{
    public int FlightpathDemandFlightpathId { get; set; }

    public int FlightpathDemandModificationId { get; set; }

    public double FlightpathDemandTime { get; set; }

    public double HeadingAngleDemandDeg { get; set; }

    public double TotalSpeedDemand { get; set; }

    public double AltitudeDemand { get; set; }

    public double PitchAngleDemandDeg { get; set; }

    public double BankAngleDemandDeg { get; set; }

    public double BankAngleRateDemandDeg { get; set; }
}