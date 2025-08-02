namespace MissionEngineering.MathLibrary;

public record PositionNED
{
    public double PositionNorth_m { get; init; }

    public double PositionEast_m { get; init; }

    public double PositionDown_m { get; init; }

    public PositionNED()
    {
    }

    public PositionNED(double positionNorth_m, double positionEast_m, double positionDown_m)
    {
        PositionNorth_m = positionNorth_m;
        PositionEast_m = positionEast_m;
        PositionDown_m = positionDown_m;
    }

    public static PositionNED operator +(PositionNED left, PositionNED right)
    {
        var positionNorth_m = left.PositionNorth_m + right.PositionNorth_m;
        var positionEast_m = left.PositionEast_m + right.PositionEast_m;
        var positionDown_m = left.PositionDown_m + right.PositionDown_m;

        var result = new PositionNED(positionNorth_m, positionEast_m, positionDown_m);

        return result;
    }

    public PositionLLA ToPositionLLA(PositionLLA positionLLAOrigin)
    {
        var positionLLA = MappingConversions.ConvertPositionNEDToPositionLLA(this, positionLLAOrigin);

        return positionLLA;
    }
}