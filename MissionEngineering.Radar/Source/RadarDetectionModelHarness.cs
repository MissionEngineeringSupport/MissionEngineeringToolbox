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
        OutputDataAll();

        LogUtilities.LogInformation(@"    Outputting Test Cases...");

        for (int i = 0; i < OutputDataList.Count; i++)
        {
            var inputData = InputData.InputDataList[i];
            var outputDataList = OutputDataList[i];

            OutputDataSingleTestCase(inputData, outputDataList);
        }


        LogUtilities.LogInformation(@"    Outputting Test Cases. Done.");
        LogUtilities.LogInformation(@"");
    }

    public void OutputDataAll()
    {
        LogUtilities.LogInformation($"    Outputting Combined Data...");

        InputData.WriteToJsonFile(InputFilePath, 12);

        LogUtilities.LogInformation($"    Outputting Combined Data. Done.");
        LogUtilities.LogInformation($"");
    }

    public void OutputDataSingleTestCase(RadarDetectionModelInputData inputData, List<RadarDetectionModelOutputData> outputDataList)
    {
        var padding = 12;

        LogUtilities.LogInformation($"        Test Case: {inputData.RadarSystemSettings.RadarSystemName}...");

        var radarName = inputData.RadarSystemSettings.RadarSystemName;

        var inputDataFileName = $@"{OutputFolder}\{ScenarioName}_{radarName}_RadarDetectionModel_InputData.json";
        var outputDataFileName = $@"{OutputFolder}\{ScenarioName}_{radarName}_RadarDetectionModel_OutputData.csv";
        var texFileName = $@"{OutputFolder}\{ScenarioName}_{radarName}_RadarDetectionModel_Report.tex";

        inputData.WriteToJsonFile(inputDataFileName, padding);
        outputDataList.WriteToCsvFile(outputDataFileName, padding);
        GenerateTexFile(texFileName, inputDataFileName, outputDataFileName, padding);

        LaTexUtilities.ConvertTexToPdf(texFileName, padding);

        LogUtilities.LogInformation($"        Test Case: {inputData.RadarSystemSettings.RadarSystemName}. Done.");
    }

    public void GenerateTexFile(string texFilePath, string jsonFilePath, string csvFilePath, int padding = 0)
    {
        var jsonFileName = Path.GetFileName(jsonFilePath);
        var csvFileName = Path.GetFileName(csvFilePath);

        var lines = new StringBuilder();

        lines.AppendLine(@"\documentclass{article}");
        lines.AppendLine(@"");
        lines.AppendLine(@"\usepackage{amsmath}");
        lines.AppendLine(@"\usepackage{graphicx}");
        lines.AppendLine(@"\usepackage{listings}");
        lines.AppendLine(@"\usepackage{xcolor}");
        lines.AppendLine(@"\usepackage{pdflscape}");
        lines.AppendLine(@"\usepackage{pgfplots}");
        lines.AppendLine(@"");
        lines.AppendLine(@"\pgfplotsset{compat=1.18}");
        lines.AppendLine(@"");
        lines.AppendLine(@"\lstdefinelanguage{json}");
        lines.AppendLine(@"{");
        lines.AppendLine(@"    basicstyle       = \ttfamily\small,");
        lines.AppendLine(@"    numbers          = left,");
        lines.AppendLine(@"    numberstyle      = \tiny\color{gray},");
        lines.AppendLine(@"    stepnumber       = 1,");
        lines.AppendLine(@"    numbersep        = 8pt,");
        lines.AppendLine(@"    showstringspaces = false,");
        lines.AppendLine(@"    breaklines       = true,");
        lines.AppendLine(@"    frame            = single,");
        lines.AppendLine(@"    backgroundcolor  = \color{lightgray!20}");
        lines.AppendLine(@"}");
        lines.AppendLine(@"");
        lines.AppendLine(@"\begin{document}");
        lines.AppendLine(@"");
        lines.AppendLine(@"\newpage");
        lines.AppendLine(@"");
        lines.AppendLine(@"\section*{Inputs}");
        lines.AppendLine(@"");
        lines.AppendLine($@"\lstinputlisting[language = json, caption = Radar Detection Model - Inputs]{{{jsonFileName}}}");
        lines.AppendLine(@"");
        lines.AppendLine(@"\newpage");
        lines.AppendLine(@"");
        lines.AppendLine(@"\section*{Outputs}");
        lines.AppendLine(@"");
        lines.AppendLine(@"\subsection{SNR vs Target Range (km)}");
        lines.AppendLine(@"");
        lines.AppendLine(@"\begin{tikzpicture}");
        lines.AppendLine(@"    \begin{axis}");
        lines.AppendLine(@"    [");
        lines.AppendLine(@"        xlabel = {Target Range (km)},");
        lines.AppendLine(@"        ylabel = {SNR (dB)},");
        lines.AppendLine(@"        title  = {SNR vs Target Range (km)},");
        lines.AppendLine(@"        grid   = both,");
        lines.AppendLine(@"        width  = 15cm,");
        lines.AppendLine(@"        height = 15cm");
        lines.AppendLine(@"    ]");
        lines.AppendLine(@"        \addplot[");
        lines.AppendLine(@"            only marks,");
        lines.AppendLine(@"            scatter, ");
        lines.AppendLine(@"            mark = *,");
        lines.AppendLine(@"            color = blue");
        lines.AppendLine(@"        ]");
        lines.AppendLine($@"       table[col sep = comma, x=TargetRange_km, y=SNR_dB]{{{csvFileName}}};");
        lines.AppendLine(@"    \end{axis}");
        lines.AppendLine(@"\end{tikzpicture}");
        lines.AppendLine(@"");
        lines.AppendLine(@"\newpage");
        lines.AppendLine(@"");
        lines.AppendLine(@"\subsection{SNR vs Target Range (NM)}");
        lines.AppendLine(@"");
        lines.AppendLine(@"\begin{tikzpicture}");
        lines.AppendLine(@"    \begin{axis}");
        lines.AppendLine(@"    [");
        lines.AppendLine(@"        xlabel = {Target Range (NM)},");
        lines.AppendLine(@"        ylabel = {SNR (dB)},");
        lines.AppendLine(@"        title  = {SNR vs Target Range (NM)},");
        lines.AppendLine(@"        grid   = both,");
        lines.AppendLine(@"        width  = 15cm,");
        lines.AppendLine(@"        height = 15cm");
        lines.AppendLine(@"    ]");
        lines.AppendLine(@"        \addplot[");
        lines.AppendLine(@"            only marks,");
        lines.AppendLine(@"            scatter, ");
        lines.AppendLine(@"            mark = *,");
        lines.AppendLine(@"            color = blue");
        lines.AppendLine(@"        ]");
        lines.AppendLine($@"       table[col sep = comma, x=TargetRange_NM, y=SNR_dB]{{{csvFileName}}};");
        lines.AppendLine(@"    \end{axis}");
        lines.AppendLine(@"\end{tikzpicture}");
        lines.AppendLine(@"");
        lines.AppendLine(@"\end{document}");

        LogUtilities.LogInformation($"Writing TeX  file : {texFilePath}", padding);

        File.WriteAllText(texFilePath, lines.ToString());
    }
}