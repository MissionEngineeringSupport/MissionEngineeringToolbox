using MissionEngineering.Core;
using MissionEngineering.Radar;
using System.Xml;
using System.Xml.XPath;

namespace MissionEngineering.Simulation;

public class Program
{
    public static string InputFileName { get; set; }

    public static string InputFilePath { get; set; }

    public static string LogFile { get; set; }

    public static bool IsValidRun { get; set; }

    public static bool IsGenerateExample { get; set; }

    public static string OutputFolder { get; set; }

    public static string ScenarioName { get; set; }

    public static RadarDetectionModelHarness RadarDetectionModelHarness { get; set; }

    public static RadarDetectionModelInputData RadarDetectionModelInputData { get; set; }

    /// <summary>
    /// Radar Detection Model Console Application
    /// </summary>
    /// <param name="isGenerateExample">If true, generates example input and output files in the current directory</param>
    /// <param name="inputFileName">The full path to the input json file.</param>
    public static void Main(bool isGenerateExample = false, string inputFileName = "")
    {
        InputFileName = inputFileName;
        IsGenerateExample = isGenerateExample;

        IsValidRun = isGenerateExample || !string.IsNullOrEmpty(InputFileName);

        if (!IsValidRun)
        {
            Console.WriteLine("Please provide a valid input file using the --input-file-name switch or set the --is-generate-example switch to true.");
            Console.WriteLine("   Use --help to list the command line options.");
            return;
        }

        Run();
    }

    private static void Run()
    {
        GenerateDetectionModelSettings();

        CreateLog();

        WriteSettings();

        ReadInputFile();

        if (!IsValidRun)
        {
            LogUtilities.LogInformation(@"Terminating Radar Detection Model.");
            LogUtilities.LogInformation(@"");
            return;
        }

        CreateDetectionModelHarness();

        RunDetectionModel();

        OutputData();
    }

    private static void GenerateDetectionModelSettings()
    {
        if (IsGenerateExample)
        {
            GenerateDetectionModelSettingsExample();
        }
        else
        {
            GenerateDetectionModelSettingsFromFile();
        }
    }
    private static void GenerateDetectionModelSettingsExample()
    {
        RadarDetectionModelInputData = RadarDetectionModelInputDataFactory.RadarDetectionModelInputData_Test_1();

        ScenarioName = RadarDetectionModelInputData.RadarSystemSettings.RadarSystemName;

        OutputFolder = Environment.CurrentDirectory;

        InputFileName = ScenarioName + "_RadarDetectionModel_InputData.json";
        InputFilePath = Path.Combine(OutputFolder, InputFileName);
    }

    private static void GenerateDetectionModelSettingsFromFile()
    {
        InputFilePath = InputFileName;

        ScenarioName = Path.GetFileNameWithoutExtension(InputFileName);

        OutputFolder = Path.GetDirectoryName(InputFileName);

        if (string.IsNullOrEmpty(OutputFolder))
        {
            OutputFolder = Environment.CurrentDirectory;
            InputFilePath = Path.Combine(OutputFolder, InputFileName);
        }
    }

    private static void CreateLog()
    {
        LogFile = Path.Combine(OutputFolder, $@"{ScenarioName}_RadarDetectionModel.log");

        LogUtilities.CreateLogger(LogFile);
    }

    private static void WriteSettings()
    {
        LogUtilities.LogInformation(@"Radar Detection Model Settings.");
        LogUtilities.LogInformation($"    Is Generate Example : {IsGenerateExample}");
        LogUtilities.LogInformation($"    Input File Name     : {InputFileName}");
        LogUtilities.LogInformation($"    Current Directory   : {Environment.CurrentDirectory}");
        LogUtilities.LogInformation($"    Output Folder       : {OutputFolder}");
        LogUtilities.LogInformation($"    Input File Path     : {InputFilePath}");
        LogUtilities.LogInformation($"    Log File            : {LogFile}");
        LogUtilities.LogInformation(@"");
    }
    private static void ReadInputFile()
    {
        if (!IsGenerateExample)
        {
            if (!File.Exists(InputFilePath))
            {
                LogUtilities.LogInformation($"Input file not found: {InputFilePath}");
                IsValidRun = false;
                return;
            }

            RadarDetectionModelInputData = JsonUtilities.ReadFromJsonFile<RadarDetectionModelInputData>(InputFilePath);
        }
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
        var inputDataFileName = $@"{OutputFolder}\{ScenarioName}_RadarDetectionModel_InputData.json";
        var outputDataFileName = $@"{OutputFolder}\{ScenarioName}_RadarDetectionModel_OutputData.csv";
        var texFileName = $@"{OutputFolder}\{ScenarioName}_RadarDetectionModel_Report.tex";

        RadarDetectionModelInputData.WriteToJsonFile(inputDataFileName);
        RadarDetectionModelHarness.OutputDataList.WriteToCsvFile(outputDataFileName);
        RadarDetectionModelHarness.GenerateTexFile(texFileName, inputDataFileName, outputDataFileName);

        LaTexUtilities.ConvertTexToPdf(texFileName);
    }
}