using MissionEngineering.Core;
using MissionEngineering.Platform;
using MissionEngineering.Simulation;
using System.Text;

namespace MissionEngineering.SimdisLibrary;

public class SimdisExporter : ISimdisExporter
{
    public SimulationData SimulationData { get; set; }

    public StringBuilder SimdisData { get; set; }

    public SimdisExporter(SimulationData simulationData)
    {
        SimulationData = simulationData;
        SimdisData = new StringBuilder();
    }

    public void GenerateSimdisData()
    {
        CreateSimdisHeader();

        CreatePlatforms();
    }

    public void WriteSimdisData()
    {
        if (!SimulationData.SimulationSettings.IsWriteData)
        {
            return;
        }

        var fileName = $"{SimulationData.SimulationSettings.SimulationName}.asi";

        var fileNameFull = SimulationData.SimulationSettings.GetFileNameFull(fileName);

        LogUtilities.LogInformation($"Writing Asi  File : {fileNameFull}");

        var strings = SimdisData.ToString();

        File.WriteAllText(fileNameFull, strings);
    }

    public void CreateSimdisHeader()
    {
        var llaOrigin = SimulationData.ScenarioSettings.LLAOrigin;

        AddLine("Version          24");
        AddLine("""Classification   "Unclassified" 0x8000FF00""");
        AddLine(@$"ScenarioInfo     ""{SimulationData.SimulationSettings.SimulationName}"" ");
        AddLine("""VerticalDatum    "WGS84" """);
        AddLine("""CoordSystem      "LLA" """);
        AddLine($"RefLLA           {llaOrigin.LatitudeDeg} {llaOrigin.LongitudeDeg} {llaOrigin.Altitude}");
        AddLine("""ReferenceTimeECI "0.0" """);
        AddLine("DegreeAngles     1");
        AddLine("");
    }

    public void CreatePlatforms()
    {
        var index = 0;

        foreach (var platformSettings in SimulationData.ScenarioSettings.PlatformSettingsList)
        {
            var platformId = platformSettings.PlatformId;

            var platformIdSimdis = GetSimdisPlatformId(platformId);

            var platformStateDataList = SimulationData.PlatformStateDataPerPlatform[index];

            CreatePlatformInitialisation(platformIdSimdis, platformSettings);

            CreatePlatformData(platformIdSimdis, platformStateDataList);

            index++;
        }
    }

    public int GetSimdisPlatformId(int platformId)
    {
        return platformId;
    }

    public void CreatePlatformInitialisation(int platformId, PlatformSettings platformSettings)
    {
        AddLine(@$"PlatformID          {platformId}");
        AddLine(@$"PlatformName        {platformId} ""{platformSettings.PlatformName}""");
        AddLine(@$"PlatformType        {platformId} ""{platformSettings.PlatformType}""");
        AddLine(@$"PlatformIcon        {platformId} ""{platformSettings.PlatformIcon}""");
        AddLine(@$"PlatformFHN         {platformId} {platformSettings.PlatformAffiliation}");
        AddLine(@$"PlatformInterpolate {platformId} {platformSettings.PlatformInterpolate}");
        AddLine(@$"PlatformCoordSystem {platformId} ""NED""");
        AddLine("");
        AddLine(@$"GenericData         {platformId} ""SIMDIS_DynamicScale"" ""1"" ""0"" ");
        AddLine(@$"GenericData         {platformId} ""SIMDIS_ScaleLevel"" ""{platformSettings.PlatformScaleLevel}"" ""0"" ");
        AddLine("");
    }

    public void CreatePlatformData(int platformId, List<PlatformStateData> platformStateDataList)
    {
        foreach (var pd in platformStateDataList)
        {
            var fd = pd.FlightpathData;

            var time = fd.FlightpathTime;
            var lla = fd.PositionLLA;
            var pos = fd.PositionNED;
            var vel = fd.VelocityNED;
            var att = fd.Attitude;

            string line = $"PlatformData {platformId} {time} {pos.PositionNorth} {pos.PositionEast} {pos.PositionDown} {att.HeadingAngleDeg} {att.PitchAngleDeg} {att.BankAngleDeg} {vel.VelocityNorth} {vel.VelocityEast} {vel.VelocityDown}";

            AddLine(line);
        }

        AddLine("");
    }

    public void AddLine(string line)
    {
        SimdisData.AppendLine(line);
    }
}