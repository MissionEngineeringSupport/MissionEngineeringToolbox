using MissionEngineering.Core;
using MissionEngineering.Radar;

namespace MissionEngineering.Simulation;

public class Program
{
    public static string OutputFolder { get; set; }

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
        var logFile = Path.Combine(OutputFolder, $@"{ScenarioName}_RadarDetectionModel.log");

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
        var inputDataFileName = $"{OutputFolder}{ScenarioName}_RadarDetectionModel_InputData.json";
        var outputDataFileName = $"{OutputFolder}{ScenarioName}_RadarDetectionModel_OutputData.csv";
        var texFileName = $"{OutputFolder}{ScenarioName}_RadarDetectionModel_Report.tex";

        RadarDetectionModelInputData.WriteToJsonFile(inputDataFileName);
        RadarDetectionModelHarness.OutputDataList.WriteToCsvFile(outputDataFileName);
        RadarDetectionModelHarness.GenerateTexFile(texFileName, inputDataFileName, outputDataFileName);

        LaTexUtilities.ConvertTexToPdf(texFileName);
    }
}