using MissionEngineering.Core;
using MissionEngineering.Radar;
using System.Xml;
using System.Xml.XPath;

namespace MissionEngineering.Simulation;

public class Program
{
    public static string InputFileName { get; set; }

    public static string InputFileNameFinal { get; set; }

    public static string InputFilePath { get; set; }

    public static string LogFile { get; set; }

    public static bool IsValidRun { get; set; }

    public static bool IsGenerateExample { get; set; }

    public static string OutputFolder { get; set; }

    public static string ScenarioName { get; set; }

    public static RadarDetectionModelHarness RadarDetectionModelHarness { get; set; }

    public static RadarDetectionModelHarnessInputData RadarDetectionModelHarnessInputData { get; set; }

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

        WriteFinished();
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
        RadarDetectionModelHarnessInputData = RadarDetectionModelHarnessInputDataFactory.Scenario_1();

        OutputFolder = Environment.CurrentDirectory;

        ScenarioName = RadarDetectionModelHarnessInputData.ScenarioName;

        InputFileNameFinal = ScenarioName + "_RadarDetectionModelHarness_InputData.json";
        InputFilePath = Path.Combine(OutputFolder, InputFileNameFinal);
    }

    private static void GenerateDetectionModelSettingsFromFile()
    {
        InputFileNameFinal = InputFileName;

        InputFilePath = InputFileName;

        ScenarioName = Path.GetFileNameWithoutExtension(InputFileName);

        ScenarioName = ScenarioName.Replace("_RadarDetectionModelHarness", "");
        ScenarioName = ScenarioName.Replace("_InputData", "");

        OutputFolder = Path.GetDirectoryName(InputFileName);

        if (string.IsNullOrEmpty(OutputFolder))
        {
            OutputFolder = Environment.CurrentDirectory;
            InputFilePath = Path.Combine(OutputFolder, InputFileName);
        }
    }

    private static void CreateLog()
    {
        LogFile = Path.Combine(OutputFolder, $@"{ScenarioName}_RadarDetectionModelHarness.log");

        LogUtilities.CreateLogger(LogFile);
    }

    private static void WriteSettings()
    {
        LogUtilities.LogInformation(@"Running Radar Detection Model Harness...");
        LogUtilities.LogInformation(@"");
        LogUtilities.LogInformation(@"    Displaying Command Line Data...");
        LogUtilities.LogInformation($"        Is Generate Example : {IsGenerateExample}");
        LogUtilities.LogInformation($"        Input File Name     : {InputFileName}");
        LogUtilities.LogInformation(@"    Displaying Command Line Data. Done.");
        LogUtilities.LogInformation(@"");
        LogUtilities.LogInformation(@"    Displaying Program Settings...");
        LogUtilities.LogInformation($"        Scenario Name       : {ScenarioName}");
        LogUtilities.LogInformation($"        Current Directory   : {Environment.CurrentDirectory}");
        LogUtilities.LogInformation($"        Output Folder       : {OutputFolder}");
        LogUtilities.LogInformation($"        Input File Name     : {InputFileNameFinal}");
        LogUtilities.LogInformation($"        Input File Path     : {InputFilePath}");
        LogUtilities.LogInformation($"        Log File            : {LogFile}");
        LogUtilities.LogInformation(@"    Displaying Settings. Done.");
        LogUtilities.LogInformation(@"");
    }
    private static void ReadInputFile()
    {
        if (!IsGenerateExample)
        {
            if (!File.Exists(InputFilePath))
            {
                LogUtilities.LogInformation($"    Input file not found: {InputFilePath}");
                IsValidRun = false;
                return;
            }

            RadarDetectionModelHarnessInputData = JsonUtilities.ReadFromJsonFile<RadarDetectionModelHarnessInputData>(InputFilePath);
        }
    }

    private static void CreateDetectionModelHarness()
    {
        RadarDetectionModelHarness = new RadarDetectionModelHarness()
        {
            InputFilePath = InputFilePath,
            OutputFolder = OutputFolder,
            InputData = RadarDetectionModelHarnessInputData,
        };
    }

    private static void RunDetectionModel()
    {
        RadarDetectionModelHarness.Run();
    }

    private static void OutputData()
    {
        RadarDetectionModelHarness.OutputData();
    }

    private static void WriteFinished()
    {
        LogUtilities.LogInformation(@"Running Radar Detection Model Harness. Done.");
        LogUtilities.LogInformation(@"");
    }
}