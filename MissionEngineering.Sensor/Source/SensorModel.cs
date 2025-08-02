using MissionEngineering.Core;
using MissionEngineering.MathLibrary;
using MissionEngineering.Platform;

namespace MissionEngineering.Sensor;

public class SensorModel
{
    public ISimulationClock SimulationClock { get; set; }

    public ILLAOrigin LLAOrigin { get; set; }

    public SensorModel(ISimulationClock simulationClock, ILLAOrigin llaOrigin)
    {
        SimulationClock = simulationClock;
        LLAOrigin = llaOrigin;
    }

    public SensorReport GenerateSensorReport(PlatformModel targetPlatform)
    {
        var sensorReport = new SensorReport();

        return sensorReport;
    }
}