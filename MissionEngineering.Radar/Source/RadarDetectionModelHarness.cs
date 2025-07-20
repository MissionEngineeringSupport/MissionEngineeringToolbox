using MissionEngineering.Core;
using MissionEngineering.MathLibrary;
using System;
using System.Drawing;
using System.Globalization;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            model.TargetRange_m = range;

            model.Run();

            OutputDataList.Add(model.OutputData);
        }
    }

    public void GenerateTexFile(string texFilePath, string jsonFilePath, string csvFilePath)
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

        LogUtilities.LogInformation($"Writing TeX  file : {texFilePath}");
        LogUtilities.LogInformation($"");

        File.WriteAllText(texFilePath, lines.ToString());
    }
}