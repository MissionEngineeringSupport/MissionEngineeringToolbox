﻿using MissionEngineering.MathLibrary;
using MissionEngineering.Platform;

namespace MissionEngineering.Simulation;

public record ScenarioSettings
{
    public string ScenarioName { get; set; }

    public SimulationClockSettings SimulationClockSettings { get; set; }

    public PositionLLA LLAOrigin { get; set; }

    public List<PlatformSettings> PlatformSettingsList { get; set; }
}