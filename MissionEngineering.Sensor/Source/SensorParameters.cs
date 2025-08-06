namespace MissionEngineering.Sensor;

public class SensorParameters
{
    public bool IsGenerateRange { get; set; }

    public bool IsGenerateRangeRate { get; set; }

    public bool IsGenerateAzimuth { get; set; }

    public bool IsGenerateElevation { get; set; }

    public bool RangeError_m { get; set; }

    public bool RangeRateError_ms { get; set; }

    public bool AzimuthError_deg { get; set; }

    public bool ElevationError_deg { get; set; }
}