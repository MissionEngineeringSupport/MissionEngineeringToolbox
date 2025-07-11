namespace MissionEngineering.MathLibrary;

public record PositionNED
{
    public double PositionNorth { get; init; }

    public double PositionEast { get; init; }

    public double PositionDown { get; init; }

    public PositionNED()
    {
    }

    public PositionNED(double positionNorth, double positionEast, double positionDown)
    {
        PositionNorth = positionNorth;
        PositionEast = positionEast;
        PositionDown = positionDown;
    }

    public static PositionNED operator +(PositionNED left, PositionNED right)
    {
        var positionNorth = left.PositionNorth + right.PositionNorth;
        var positionEast = left.PositionEast + right.PositionEast;
        var positionDown = left.PositionDown + right.PositionDown;

        var result = new PositionNED(positionNorth, positionEast, positionDown);

        return result;
    }

    //public static PositionNED operator -(PositionNED left)
    //{
    //    var positionNorth = -left.PositionNorth;
    //    var positionEast = -left.PositionEast;
    //    var positionDown = -left.PositionDown;

    //    var result = new PositionNED(positionNorth, positionEast, positionDown);

    //    return result;
    //}

    //public static PositionNED operator -(PositionNED left, PositionNED right)
    //{
    //    var positionNorth = left.PositionNorth - right.PositionNorth;
    //    var positionEast = left.PositionEast - right.PositionEast;
    //    var positionDown = left.PositionDown - right.PositionDown;

    //    var result = new PositionNED(positionNorth, positionEast, positionDown);

    //    return result;
    //}

    //public static PositionNED operator *(PositionNED left, double right)
    //{
    //    var positionNorth = left.PositionNorth * right;
    //    var positionEast = left.PositionEast * right;
    //    var positionDown = left.PositionDown * right;

    //    var result = new PositionNED(positionNorth, positionEast, positionDown);

    //    return result;
    //}

    //public static PositionNED operator *(double left, PositionNED right)
    //{
    //    var positionNorth = left * right.PositionNorth;
    //    var positionEast = left * right.PositionEast;
    //    var positionDown = left * right.PositionDown;

    //    var result = new PositionNED(positionNorth, positionEast, positionDown);

    //    return result;
    //}

    //public static PositionNED operator /(PositionNED left, double right)
    //{
    //    var positionNorth = left.PositionNorth / right;
    //    var positionEast = left.PositionEast / right;
    //    var positionDown = left.PositionDown / right;

    //    var result = new PositionNED(positionNorth, positionEast, positionDown);

    //    return result;
    //}

    //public static PositionNED operator /(double left, PositionNED right)
    //{
    //    var positionNorth = left / right.PositionNorth;
    //    var positionEast = left / right.PositionEast;
    //    var positionDown = left / right.PositionDown;

    //    var result = new PositionNED(positionNorth, positionEast, positionDown);

    //    return result;
    //}

    public PositionLLA ToPositionLLA(PositionLLA positionLLAOrigin)
    {
        var positionLLA = MappingConversions.ConvertPositionNEDToPositionLLA(this, positionLLAOrigin);

        return positionLLA;
    }
}