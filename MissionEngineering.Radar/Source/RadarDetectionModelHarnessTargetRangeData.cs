namespace MissionEngineering.Radar;

public class RadarDetectionModelHarnessTargetRangeData
{
    public double TargetRangeStart { get; set; }

    public double TargetRangeEnd { get; set; }

    public double TargetRangeStep { get; set; }

    public RadarDetectionModelHarnessTargetRangeData()
    {
        TargetRangeStart = 0.0;
        TargetRangeEnd = 10000.0;
        TargetRangeStep = 100.0;
    }
}