namespace MissionEngineering.Radar;

public class RadarDetectionModelHarnessInputData
{
    public string ScenarioName { get; set; }

    public List<RadarDetectionModelInputData> InputDataList { get; set; }

    public RadarDetectionModelHarnessTargetRangeData TargetRangeData { get; set; }

    public RadarDetectionModelHarnessInputData()
    {
    }
}