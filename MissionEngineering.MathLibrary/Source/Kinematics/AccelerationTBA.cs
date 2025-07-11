namespace MissionEngineering.MathLibrary;

public record AccelerationTBA
{
    public double AccelerationAxial { get; init; }

    public double AccelerationLateral { get; init; }

    public double AccelerationVertical { get; init; }

    public AccelerationTBA()
    {
    }

    public AccelerationTBA(double accelerationAxial, double accelerationLateral, double accelerationVertical)
    {
        AccelerationAxial = accelerationAxial;
        AccelerationLateral = accelerationLateral;
        AccelerationVertical = accelerationVertical;
    }

    public Vector ToVector()
    {
        var result = new Vector(AccelerationAxial, AccelerationLateral, AccelerationVertical);

        return result;
    }
}