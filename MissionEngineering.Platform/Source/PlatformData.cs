using MissionEngineering.Core;

namespace MissionEngineering.Platform;

public class PlatformData
{
    public ModelTimeStamp ModelTimeStamp { get; set; }

    public PlatformSettings PlatformSettings { get; set; }

    public FlightpathData FlightpathData { get; set; }
}