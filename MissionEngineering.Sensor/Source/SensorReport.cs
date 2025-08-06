using MissionEngineering.MathLibrary;

namespace MissionEngineering.Sensor;

public class SensorReport
{
    public double DetectionTime { get; set; }

    public int SensorPlatformId { get; set; }

    public string SensorPlatformName { get; set; }

    public int SensorId { get; set; }

    public string SensorName { get; set; }

    public int TargetPlatformId { get; set; }

    public int TargetPlatformName { get; set; }

    public PositionLLA TargetPositionLLA { get; set; }

    public PositionNED TargetPositionNED { get; set; }

    public VelocityNED TargetVelocityNED { get; set; }

    public bool IsTargetRangeValid { get; set; }

    public bool IsTargetRangeRateValid { get; set; }

    public bool IsTargetAzimuthValid { get; set; }

    public bool IsTargetElevationValid { get; set; }

    public double TargetRange_m { get; set; }

    public double TargetRangeRate_ms { get; set; }

    public double TargetAzimuth_deg { get; set; }

    public double TargetElevation_deg { get; set; }

    public SensorReport()
    {
    }
}