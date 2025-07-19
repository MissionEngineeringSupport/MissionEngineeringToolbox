using System.Diagnostics;

namespace MissionEngineering.Core;

public static class LaTexUtilities
{
    public static void ConvertTexToPdf(string texFile)
    {
        LogUtilities.LogInformation("Converting Tex to PDF. Starting...");
        LogUtilities.LogInformation("");

        var path = Path.GetDirectoryName(texFile);
        var fileNameTex = Path.GetFileName(texFile);

        var pdfFile = texFile.Replace(".tex", ".pdf");

        LogUtilities.LogInformation($"     Input Tex file = {texFile}");
        LogUtilities.LogInformation($"    Output Pdf file = {pdfFile}");
        LogUtilities.LogInformation("");

        Process process = new Process();

        process.StartInfo.FileName = "pdflatex.exe";
        process.StartInfo.WorkingDirectory = path;
        process.StartInfo.Arguments = fileNameTex;

        process.Start();

        process.WaitForExit();

        LogUtilities.LogInformation("Converting Tex to PDF. Done.");
        LogUtilities.LogInformation("");
    }
}