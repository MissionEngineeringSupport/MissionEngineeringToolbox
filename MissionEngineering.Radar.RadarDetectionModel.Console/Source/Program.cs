using MissionEngineering.Core;
using MissionEngineering.Radar;

namespace MissionEngineering.Simulation;

public class Program
{
    public static string OutputFolder {  get; set; }

    public static string ScenarioName { get; set; }

    public static RadarDetectionModelHarness RadarDetectionModelHarness { get; set; }

    public static RadarDetectionModelInputData RadarDetectionModelInputData { get; set; }

    public static void Main()
    {
        Run();
    }

    private static void Run()
    {
        GenerateDetectionModelSettings();

        SetOutputFolder();

        CreateLog();

        CreateDetectionModelHarness();

        RunDetectionModel();

        OutputData();
    }

    private static void GenerateDetectionModelSettings()
    {
        RadarDetectionModelInputData = RadarDetectionModelInputDataFactory.RadarDetectionModelInputData_Test_1();

        ScenarioName = RadarDetectionModelInputData.RadarSystemSettings.RadarSystemName;
    }

    private static void SetOutputFolder()
    {
        OutputFolder = @"C:\Temp\MissionEngineeringToolbox\RadarDetectionModel\";
    }

    private static void CreateLog()
    {
        var logFile = Path.Combine(OutputFolder, @"{ScenarioName}.log");

        LogUtilities.CreateLogger(logFile);
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

    private static void RunDetectionModel()
    {
        RadarDetectionModelHarness.Run();
    }

    private static void OutputData()
    {
        var outputFolder = @"C:\Temp\MissionEngineeringToolbox\RadarDetectionModel\";

        var inputDataFileName = outputFolder + @"Radar_Test_1_DetectionModel_InputData.json";

        RadarDetectionModelInputData.WriteToJsonFile(inputDataFileName);

        var outputDataFileName = outputFolder + @"Radar_Test_1_DetectionModel_OutputData.csv";

        RadarDetectionModelHarness.OutputDataList.WriteToCsvFile(outputDataFileName);

        var texFileName = outputFolder + @"GenerateJSON.tex";

        LaTexUtilities.ConvertTexToPdf(texFileName);
    }
}