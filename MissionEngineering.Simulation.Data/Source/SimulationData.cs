using MissionEngineering.Platform;

namespace MissionEngineering.Simulation;

public class SimulationData
{
    public SimulationSettings SimulationSettings { get; set; }

    public ScenarioSettings ScenarioSettings { get; set; }

    public List<FlightpathData> PlatformDataAll { get; set; }

    public List<List<FlightpathData>> PlatformDataPerPlatform { get; set; }

    public SimulationData(SimulationSettings simulationSettings)
    {
        SimulationSettings = simulationSettings;
    }
}