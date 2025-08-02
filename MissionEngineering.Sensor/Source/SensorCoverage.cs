using MissionEngineering.MathLibrary;

namespace MissionEngineering.Sensor;

public class SensorCoverage
{
    public double RangeCoverage_m { get; set; }

    public double RangeCoverage_km { get => RangeCoverage_m.MetersToKilometers(); set => RangeCoverage_m = value.KilometersToMeters(); }

    public double AzimuthFieldOfView_deg { get; set; }

    public double ElevationFieldOfView_deg { get; set; }
}