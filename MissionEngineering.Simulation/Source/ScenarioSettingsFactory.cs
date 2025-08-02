﻿using MissionEngineering.MathLibrary;
using MissionEngineering.Platform;

namespace MissionEngineering.Simulation;

public static class ScenarioSettingsFactory
{
    public static ScenarioSettings ScenarioSettings_Test_1()
    {
        var dateTimeOrigin = new DateTime(2024, 12, 24, 15, 45, 10, 123);

        var simulationClockSettings = new SimulationClockSettings()
        {
            DateTimeOrigin = dateTimeOrigin,
            TimeStart = 10.0,
            TimeEnd = 200.0,
            TimeStep = 0.1
        };

        var llaOrigin = new PositionLLA()
        {
            Latitude_deg = 55.1,
            Longitude_deg = 12.0,
            Altitude_m = 0.0
        };

        var ps1 = PlatformSettingsFactory.PlatformSettings_1();
        var ps2 = PlatformSettingsFactory.PlatformSettings_2();

        var scenarioSettings = new ScenarioSettings()
        {
            ScenarioName = "Scenario_Test_1",
            SimulationClockSettings = simulationClockSettings,
            LLAOrigin = llaOrigin,
            PlatformSettingsList = [ps1, ps2]
        };

        return scenarioSettings;
    }
}