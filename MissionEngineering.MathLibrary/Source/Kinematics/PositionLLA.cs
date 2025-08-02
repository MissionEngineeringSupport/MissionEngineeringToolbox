namespace MissionEngineering.MathLibrary;

public record PositionLLA
{
    public double Latitude_deg { get; init; }

    public double Longitude_deg { get; init; }

    public double Altitude_m { get; init; }

    public double Altitude_ft => Altitude_m * UnitConversions.MeterToFoot;

    public double FlightLevel => UnitConversions.AltitudeFeetToFlightLevel(Altitude_ft);

    public PositionLLA()
    {
    }

    public PositionLLA(double latitude_deg, double longitude_deg, double altitude_m)
    {
        Latitude_deg = latitude_deg;
        Longitude_deg = longitude_deg;
        Altitude_m = altitude_m;
    }
}