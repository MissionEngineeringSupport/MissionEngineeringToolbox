using BenchmarkDotNet.Attributes;
using Microsoft.VSDiagnostics;

namespace MissionEngineering.Simulation;

public class SimulationRunner
{
    public int NumberOfRuns { get; set; }

    public SimulationSettings SimulationSettings { get; set; }

    public ScenarioSettings ScenarioSettings { get; set; }

    public ISimulationHarness SimulationHarness { get; set; }

    public SimulationRunner()
    {
    }

    [Benchmark]
    public void Run()
    {
        NumberOfRuns = 1;

        GenerateSimulationSettings();
        GenerateScenarioSettings();

        SimulationHarness = SimulationBuilder.CreateSimulationHarness();

        SimulationHarness.SimulationSettings = SimulationSettings;
        SimulationHarness.ScenarioSettings = ScenarioSettings;
        SimulationHarness.SimulationHarnessSettings.NumberOfRuns = NumberOfRuns;

        SimulationHarness.Run();
    }

    private void GenerateSimulationSettings()
    {
        if (NumberOfRuns == 1)
        {
            SimulationSettings = SimulationSettingsFactory.SimulationSettings_Test_1_Single();
        }
        else
        {
            SimulationSettings = SimulationSettingsFactory.SimulationSettings_Test_1_Multiple();
        }
    }

    private void GenerateScenarioSettings()
    {
        ScenarioSettings = ScenarioSettingsFactory.ScenarioSettings_Test_1();
    }
}