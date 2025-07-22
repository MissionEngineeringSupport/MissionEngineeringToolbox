using MissionEngineering.Core;
using System.Text;

namespace MissionEngineering.Radar;

public static class RadarDetectionModelTexUtilities
{
    public static string GenerateTexStringSingleTestCase(RadarDetectionModelInputData inputData, string jsonFilePath, string csvFilePath)
    {
        var jsonFileName = Path.GetFileName(jsonFilePath);
        var csvFileName = Path.GetFileName(csvFilePath);

        var lines = new StringBuilder();

        var colorString = inputData.RadarSystemSettings.RadarSystemColor;
        var legendName = inputData.RadarSystemSettings.RadarSystemName.Replace("_", " ");

        CreateDocumentHeader(lines, jsonFileName);
        AddPlot(lines, csvFileName, legendName, colorString);
        CreateDocumentFooter(lines);

        var texString = lines.ToString();

        return texString;
    }

    public static string GenerateTexStringCombined(RadarDetectionModelHarnessInputData inputData, string outputFolder, string scenarioName)
    {
        var inputDataSingleTestCase = inputData.InputDataList.FirstOrDefault();

        var radarName1 = inputDataSingleTestCase.RadarSystemSettings.RadarSystemName;

        var lines = new StringBuilder();

        var jsonFileName = $@"{scenarioName}_{radarName1}_RadarDetectionModel_InputData.json";

        CreateDocumentHeader(lines, jsonFileName);
        
        foreach (var i in inputData.InputDataList)
        {
            var colorString = i.RadarSystemSettings.RadarSystemColor;
            var legendName = i.RadarSystemSettings.RadarSystemName.Replace("_", " ");
            var radarName = i.RadarSystemSettings.RadarSystemName;

            var csvFileName = $@"{scenarioName}_{radarName}_RadarDetectionModel_OutputData.csv";

            AddPlot(lines, csvFileName, legendName, colorString);
        }

        CreateDocumentFooter(lines);

        var texString = lines.ToString();

        return texString;
    }

    public static void CreateDocumentHeader(StringBuilder lines, string jsonFileName)
    {
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
    }

    public static void AddPlot(StringBuilder lines, string csvFileName, string legendName, string colorString)
    {
        lines.AppendLine(@"        \addplot[");
        lines.AppendLine(@"            mark = square,");
        lines.AppendLine($@"            color = {colorString}");
        lines.AppendLine(@"        ]");
        lines.AppendLine($@"        table[col sep = comma, x=TargetRange_km, y=SNR_dB]{{{csvFileName}}};");
        lines.AppendLine($@"        \addlegendentry{{{legendName}}}");
    }

    public static void CreateDocumentFooter(StringBuilder lines)
    {
        lines.AppendLine(@"    \end{axis}");
        lines.AppendLine(@"\end{tikzpicture}");
        lines.AppendLine(@"");
        lines.AppendLine(@"\end{document}");
    }
}