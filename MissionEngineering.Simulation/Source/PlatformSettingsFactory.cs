using MissionEngineering.MathLibrary;
using MissionEngineering.Platform;

namespace MissionEngineering.Simulation;

public static class PlatformSettingsFactory
{
    public static PlatformSettings PlatformSettings_Test_1()
    {
        var platformSettings = new PlatformSettings()
        {
            PlatformId = 1,
            PlatformName = "Flightpath_1",
        };

        return platformSettings;
    }
}