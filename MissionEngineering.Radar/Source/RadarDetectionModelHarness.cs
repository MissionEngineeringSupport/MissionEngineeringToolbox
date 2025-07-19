using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar;

public class RadarDetectionModelHarness
{
    public RadarDetectionModelInputData InputData { get; set; }

    public List<RadarDetectionModelOutputData> OutputDataList { get; set; }

    public double TargetRangeStart { get; set; }

    public double TargetRangeEnd { get; set; }

    public double TargetRangeStep { get; set; }

    public Vector TargetRanges { get; set; }

    public RadarDetectionModelHarness()
    {
        OutputDataList = new List<RadarDetectionModelOutputData>();
        TargetRangeStart = 0.0;
        TargetRangeEnd = 10000.0;
        TargetRangeStep = 100.0;
    }

    public void Run()
    {
        TargetRanges = Vector.LinearlySpacedVector(TargetRangeStart, TargetRangeEnd, TargetRangeStep);

        OutputDataList = new List<RadarDetectionModelOutputData>(TargetRanges.NumberOfElements);

        var model = new RadarDetectionModel
        {
            InputData = InputData
        };

        foreach (double range in TargetRanges)
        {
            InputData.RadarTargetSettings.TargetRange_m = range;

            model.Run();

            OutputDataList.Add(model.OutputData);
        }
    }
}