namespace MissionEngineering.MathLibrary;

public record PositionLLA
{
    public double LatitudeDeg { get; init; }

    public double LongitudeDeg { get; init; }

    public double Altitude { get; init; }

    public double Altitude_ft => Altitude * UnitConversions.MeterToFoot;

    public double FlightLevel => UnitConversions.AltitudeFeetToFlightLevel(Altitude_ft);

    public PositionLLA()
    {
    }

    public PositionLLA(double latitudeDeg, double longitudeDeg, double altitude)
    {
        LatitudeDeg = latitudeDeg;
        LongitudeDeg = longitudeDeg;
        Altitude = altitude;
    }
}