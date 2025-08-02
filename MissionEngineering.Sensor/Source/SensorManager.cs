using MissionEngineering.Platform;

namespace MissionEngineering.Sensor;

public class SensorManager
{
    public List<PlatformModel> Platforms { get; set; }

    public List<SensorModel> Sensors { get; set; }

    public List<SensorReport> SensorReports { get; set; }

    public void GenerateSensorReports()
    {
        SensorReports.Clear();

        foreach (var sensor in Sensors)
        {
            foreach (var platform in Platforms)
            {
                var sensorReport = sensor.GenerateSensorReport(platform);

                SensorReports.Add(sensorReport);
            }
        }
    }
}