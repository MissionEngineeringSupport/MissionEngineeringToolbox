using MissionEngineering.Core;
using MissionEngineering.MathLibrary;
using Serilog.Core;
using System.Text;

namespace MissionEngineering.Radar;

public class RadarDetectionModelHarness
{
    public RadarDetectionModelHarnessInputData InputData { get; set; }

    public List<List<RadarDetectionModelOutputData>> OutputDataList { get; set; }

    public Vector TargetRanges { get; set; }

    public int NumberOfTestCases => InputData.InputDataList.Count;

    public string OutputFolder { get; set; }

    public string InputFilePath { get; set; }

    public string ScenarioName => InputData.ScenarioName;

    public RadarDetectionModelHarness()
    {
    }

    public void Run()
    {
        LogUtilities.LogInformation(@"    Running Test Cases...");

        TargetRanges = Vector.LinearlySpacedVector(InputData.TargetRangeData.TargetRangeStart, InputData.TargetRangeData.TargetRangeEnd, InputData.TargetRangeData.TargetRangeStep);

        OutputDataList = new List<List<RadarDetectionModelOutputData>>(NumberOfTestCases);

        foreach (var inputData in InputData.InputDataList)
        {
            var outputDataList = RunSingleTestCase(inputData);

            OutputDataList.Add(outputDataList);
        }

        LogUtilities.LogInformation(@"    Running Test Cases. Done.");
        LogUtilities.LogInformation(@"");
    }

    public List<RadarDetectionModelOutputData> RunSingleTestCase(RadarDetectionModelInputData inputData)
    {
        var model = new RadarDetectionModel
        {
            InputData = inputData
        };

        LogUtilities.LogInformation($"        Test Case: {inputData.RadarSystemSettings.RadarSystemName}");

        var outputDataList = new List<RadarDetectionModelOutputData>(TargetRanges.NumberOfElements);

        foreach (double range in TargetRanges)
        {
            model.TargetRange_m = range;

            model.Run();

            outputDataList.Add(model.OutputData);
        }

        return outputDataList;
    }

    public void OutputData()
    {
        LogUtilities.LogInformation(@"    Outputting Test Cases...");

        for (int i = 0; i < OutputDataList.Count; i++)
        {
            var inputData = InputData.InputDataList[i];
            var outputDataList = OutputDataList[i];

            OutputDataSingleTestCase(inputData, outputDataList);
        }

        LogUtilities.LogInformation(@"    Outputting Test Cases. Done.");
        LogUtilities.LogInformation(@"");

        OutputDataCombined(InputData);
    }

    public void OutputDataSingleTestCase(RadarDetectionModelInputData inputData, List<RadarDetectionModelOutputData> outputDataList)
    {
        var padding = 12;

        LogUtilities.LogInformation($"        Test Case: {inputData.RadarSystemSettings.RadarSystemName}...");

        var radarName = inputData.RadarSystemSettings.RadarSystemName;

        var jsonDataFileName = $@"{OutputFolder}\{ScenarioName}_{radarName}_RadarDetectionModel_InputData.json";
        var csvDataFileName = $@"{OutputFolder}\{ScenarioName}_{radarName}_RadarDetectionModel_OutputData.csv";
        var texFileName = $@"{OutputFolder}\{ScenarioName}_{radarName}_RadarDetectionModel_Report.tex";

        inputData.WriteToJsonFile(jsonDataFileName, padding);
        outputDataList.WriteToCsvFile(csvDataFileName, padding);

        GenerateTexFileSingleTestCase(inputData, texFileName, jsonDataFileName, csvDataFileName, padding);

        LaTexUtilities.ConvertTexToPdf(texFileName, padding);

        LogUtilities.LogInformation($"        Test Case: {inputData.RadarSystemSettings.RadarSystemName}. Done.");
    }

    public void GenerateTexFileSingleTestCase(RadarDetectionModelInputData inputData, string texFilePath, string jsonFilePath, string csvFilePath, int padding = 0)
    {
        var texString = RadarDetectionModelTexUtilities.GenerateTexStringSingleTestCase(inputData, jsonFilePath, csvFilePath);

        LogUtilities.LogInformation($"Writing TeX  file : {texFilePath}", padding);

        File.WriteAllText(texFilePath, texString);
    }

    public void OutputDataCombined(RadarDetectionModelHarnessInputData inputData)
    {
        var padding = 12;

        LogUtilities.LogInformation($"    Outputting Combined Data...");

        InputData.WriteToJsonFile(InputFilePath, 12);

        var texFileName = $@"{OutputFolder}\{ScenarioName}_RadarDetectionModelHarness_Report.tex";

        GenerateTexFileCombined(InputData, texFileName, padding);

        LaTexUtilities.ConvertTexToPdf(texFileName, padding);

        LogUtilities.LogInformation($"    Outputting Combined Data. Done.");
        LogUtilities.LogInformation($"");
    }

    public void GenerateTexFileCombined(RadarDetectionModelHarnessInputData inputData, string texFilePath, int padding = 0)
    {
        var texString = RadarDetectionModelTexUtilities.GenerateTexStringCombined(inputData, OutputFolder, ScenarioName);

        LogUtilities.LogInformation($"Writing TeX  file : {texFilePath}", padding);

        File.WriteAllText(texFilePath, texString);
    }
}