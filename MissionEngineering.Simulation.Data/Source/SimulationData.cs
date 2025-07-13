using MissionEngineering.Platform;

namespace MissionEngineering.Simulation;

public class SimulationData
{
    public SimulationSettings SimulationSettings { get; set; }

    public ScenarioSettings ScenarioSettings { get; set; }

    public List<PlatformStateData> PlatformStateDataAll { get; set; }

    public List<List<PlatformStateData>> PlatformStateDataPerPlatform { get; set; }

    public SimulationData(SimulationSettings simulationSettings)
    {
        SimulationSettings = simulationSettings;
    }
}