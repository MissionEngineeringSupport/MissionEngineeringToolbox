using BenchmarkDotNet.Running;

namespace MissionEngineering.Simulation;

/// <summary>
///
/// </summary>
public class Program
{
    public static void Main()
    {
        var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
    }
}