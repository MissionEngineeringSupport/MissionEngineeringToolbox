using MissionEngineering.MathLibrary;

namespace MissionEngineering.Platform;

public static class PlatformSettingsFactory
{
    public static PlatformSettings PlatformSettings_Test_1()
    {
        var platformSettings = new PlatformSettings()
        {
            PlatformId = 1,
            PlatformName = "Flightpath_1",
            PlatformDescription = "Flightpath 1 Description",
            PlatformType = "Aircraft",
            PlatformIcon = "F-35A",
            PlatformAffiliation = PlatformAffiliation.Friendly,
            PlatformColor = "Red",
            PlatformInterpolate = "1",
            PlatformScaleLevel = "4.0",
            PositionNED = new PositionNED(1000.0, 2000.0, -1000.0),
            VelocityNED = new VelocityNED(-200.0, 200.0, 0.0)
        };

        return platformSettings;
    }
}