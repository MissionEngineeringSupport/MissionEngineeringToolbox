using MissionEngineering.Core;
using MissionEngineering.Radar;

namespace MissionEngineering.Simulation;

public class Program
{
    public static RadarDetectionModelHarness RadarDetectionModelHarness { get; set; }

    public static RadarDetectionModelInputData RadarDetectionModelInputData { get; set; }

    public static void Main()
    {
        Run();
    }

    private static void Run()
    {
        GenerateDetectionModelSettings();

        CreateDetectionModelHarness();

        RunDetectionModel();

        OutputData();
    }

    private static void CreateDetectionModelHarness()
    {
        RadarDetectionModelHarness = new RadarDetectionModelHarness()
        { 
            InputData = RadarDetectionModelInputData,
            TargetRangeStart = 10000.0,
            TargetRangeEnd = 200000.0,
            TargetRangeStep = 1000.0
        };
    }

    private static void GenerateDetectionModelSettings()
    {
        RadarDetectionModelInputData = RadarDetectionModelInputDataFactory.RadarDetectionModelInputData_Test_1();
    }

    private static void RunDetectionModel()
    {
        RadarDetectionModelHarness.Run();
    }

    private static void OutputData()
    {
        var inputDataFileName = @"C:\Temp\MissionEngineeringToolbox\RadarDetectionModel\Radar_Test_1_DetectionModel_InputData.json";

        RadarDetectionModelInputData.WriteToJsonFile(inputDataFileName);

        var outputDataFileName = @"C:\Temp\MissionEngineeringToolbox\RadarDetectionModel\Radar_Test_1_DetectionModel_OutputData.csv";

        RadarDetectionModelHarness.OutputDataList.WriteToCsvFile(outputDataFileName);
    }
}