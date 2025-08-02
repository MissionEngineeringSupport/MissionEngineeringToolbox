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
        AddLine($"RefLLA           {llaOrigin.Latitude_deg} {llaOrigin.Longitude_deg} {llaOrigin.Altitude_m}");
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

            var platformDataList = SimulationData.PlatformDataPerPlatform[index];

            CreatePlatformInitialisation(platformIdSimdis, platformSettings);

            CreatePlatformData(platformIdSimdis, platformDataList);

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

    public void CreatePlatformData(int platformId, List<FlightpathData> flightpathDataList)
    {
        foreach (var fd in flightpathDataList)
        {
            var time = fd.TimeStamp.SimulationTime;
            var lla = fd.PositionLLA;
            var pos = fd.PositionNED;
            var vel = fd.VelocityNED;
            var att = fd.Attitude;

            string line = $"PlatformData {platformId} {time} {pos.PositionNorth_m} {pos.PositionEast_m} {pos.PositionDown_m} {att.HeadingAngle_deg} {att.PitchAngle_deg} {att.BankAngle_deg} {vel.VelocityNorth_ms} {vel.VelocityEast_ms} {vel.VelocityDown_ms}";

            AddLine(line);
        }

        AddLine("");
    }

    public void AddLine(string line)
    {
        SimdisData.AppendLine(line);
    }
}