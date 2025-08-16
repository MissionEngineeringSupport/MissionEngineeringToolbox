using BenchmarkDotNet.Running;

namespace MissionEngineering.Simulation;

/// <summary>
///
/// </summary>
public class Program
{
    public static void Main()
    {
        var isBenchmark = false;

        if (isBenchmark)
        { 
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }
        else
        {
            var simulationRunner = new SimulationRunner();

            simulationRunner.Run();
        }
    }
}