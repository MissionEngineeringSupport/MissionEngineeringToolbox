using MissionEngineering.Core;

namespace MissionEngineering.Platform;

public class PlatformStateData : ISimulationModelState
{
    public SimulationModelTimeStamp SimulationModelTimeStamp { get; set; }

    public PlatformSettings PlatformSettings { get; set; }

    public FlightpathData FlightpathData { get; set; }
}