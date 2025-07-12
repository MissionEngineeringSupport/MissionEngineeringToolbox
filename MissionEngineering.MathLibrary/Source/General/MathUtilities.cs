using CommunityToolkit.Diagnostics;

namespace MissionEngineering.MathLibrary;

public class MathUtilities
{
    public static double FloorToStepSize(double value, double stepSize)
    {
        Guard.IsGreaterThan(stepSize, 0, nameof(stepSize));

        return Math.Floor(value / stepSize) * stepSize;
    }

    public static double RoundToStepSize(double value, double stepSize)
    {
        Guard.IsGreaterThan(stepSize, 0, nameof(stepSize));

        return Math.Round(value / stepSize) * stepSize;
    }
}