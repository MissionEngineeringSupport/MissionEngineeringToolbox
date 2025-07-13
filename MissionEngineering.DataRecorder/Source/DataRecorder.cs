using MissionEngineering.Core;
using MissionEngineering.SimdisLibrary;
using MissionEngineering.Simulation;

namespace MissionEngineering.DataRecorder;

public class DataRecorder : IDataRecorder
{
    public SimulationData SimulationData { get; set; }

    public ISimdisExporter SimdisExporter { get; set; }

    public DataRecorder(SimulationData simulationData, ISimdisExporter simdisExporter)
    {
        SimulationData = simulationData;
        SimdisExporter = simdisExporter;
    }

    public void Initialise(double time)
    {
        SimulationData.PlatformStateDataAll = [];
        SimulationData.PlatformStateDataPerPlatform = [];
    }

    public void Finalise(double time)
    {
        CreatePlatformData();

        WriteData();
    }

    public void WriteData()
    {
        if (!SimulationData.SimulationSettings.IsWriteData)
        {
            return;
        }

        CreateOutputFolder();

        WriteJsonData();
        WriteCsvData();
        WriteSimdisData();
    }

    public void CreateOutputFolder()
    {
        if (Directory.Exists(SimulationData.SimulationSettings.OutputFolder))
        {
            return;
        }

        Directory.CreateDirectory(SimulationData.SimulationSettings.OutputFolder);
    }

    public void CreatePlatformData()
    {
        foreach (var platformSettings in SimulationData.ScenarioSettings.PlatformSettingsList)
        {
            var platformId = platformSettings.PlatformId;

            var platformData = SimulationData.PlatformStateDataAll.Where(s => s.PlatformSettings.PlatformId == platformId).ToList();

            SimulationData.PlatformStateDataPerPlatform.Add(platformData);
        }
    }

    public void WriteJsonData()
    {
        WriteSimulationSettingsToJson();
        WriteScenarioSettingsToJson();
    }

    public void WriteCsvData()
    {
        WritePlatformDataAllToCsv();
        WritePlatformDataPerPlatformToCsv();
    }

    public void WriteSimdisData()
    {
        SimdisExporter.GenerateSimdisData();
        SimdisExporter.WriteSimdisData();
    }

    public void WriteSimulationSettingsToJson()
    {
        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_SimulationSettings.json";

        var fileNameFull = GetFileNameFull(fileName);

        SimulationData.SimulationSettings.WriteToJsonFile(fileNameFull);
    }

    public void WriteScenarioSettingsToJson()
    {
        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_ScenarioSettings.json";

        var fileNameFull = GetFileNameFull(fileName);

        SimulationData.ScenarioSettings.WriteToJsonFile(fileNameFull);
    }

    public void WritePlatformDataAllToCsv()
    {
        var platformDataAll = SimulationData.PlatformStateDataAll;

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}_PlatformData_All.csv";

        var fileNameFull = GetFileNameFull(fileName);

        platformDataAll.WriteToCsvFile(fileNameFull);
    }

    public void WritePlatformDataPerPlatformToCsv()
    {
        int index = 0;

        foreach (var platformSettings in SimulationData.ScenarioSettings.PlatformSettingsList)
        {
            var platformDataList = SimulationData.PlatformStateDataPerPlatform[index];

            var fileName = $"{SimulationData.SimulationSettings.SimulationName}_PlatformData_{platformSettings.PlatformName}.csv";

            var fileNameFull = GetFileNameFull(fileName);

            platformDataList.WriteToCsvFile(fileNameFull);

            index++;
        }
    }

    public string GetFileNameFull(string fileName)
    {
        var fileNameFull = SimulationData.SimulationSettings.GetFileNameFull(fileName);

        return fileNameFull;
    }
}