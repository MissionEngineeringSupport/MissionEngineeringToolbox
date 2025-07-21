using CsvHelper;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace MissionEngineering.Core;

public static class CsvUtilities
{
    public static void WriteToCsvFile<T>(this IEnumerable<T> records, string fileName, int padding = 0)
    {
        LogUtilities.LogInformation($"Writing Csv  File : {fileName}", padding);

        using var writer = new StreamWriter(fileName);

        using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);

        var options = new TypeConverterOptions { Formats = ["yyyy-MM-ddTHH:mm:ss.fffZ"] };
        csvWriter.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);

        csvWriter.WriteRecords(records);
    }
}