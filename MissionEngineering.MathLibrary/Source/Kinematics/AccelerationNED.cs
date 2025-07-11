namespace MissionEngineering.MathLibrary;

public record AccelerationNED
{
    public double AccelerationNorth { get; init; }

    public double AccelerationEast { get; init; }

    public double AccelerationDown { get; init; }

    public AccelerationNED()
    {
    }

    public AccelerationNED(double accelerationNorth, double accelerationEast, double accelerationDown)
    {
        AccelerationNorth = accelerationNorth;
        AccelerationEast = accelerationEast;
        AccelerationDown = accelerationDown;
    }

    public AccelerationNED(double[] accelerationNED)
    {
        AccelerationNorth = accelerationNED[0];
        AccelerationEast = accelerationNED[1];
        AccelerationDown = accelerationNED[2];
    }

    public AccelerationNED(Vector accelerationNED) : this(accelerationNED.Data)
    {
    }

    public static AccelerationNED operator +(AccelerationNED left, AccelerationNED right)
    {
        var accelerationNorth = left.AccelerationNorth + right.AccelerationNorth;
        var accelerationEast = left.AccelerationEast + right.AccelerationEast;
        var accelerationDown = left.AccelerationDown + right.AccelerationDown;

        var result = new AccelerationNED(accelerationNorth, accelerationEast, accelerationDown);

        return result;
    }

    //public static AccelerationNED operator -(AccelerationNED left)
    //{
    //    var accelerationNorth = -left.AccelerationNorth;
    //    var accelerationEast = -left.AccelerationEast;
    //    var accelerationDown = -left.AccelerationDown;

    //    var result = new AccelerationNED(accelerationNorth, accelerationEast, accelerationDown);

    //    return result;
    //}

    //public static AccelerationNED operator -(AccelerationNED left, AccelerationNED right)
    //{
    //    var accelerationNorth = left.AccelerationNorth - right.AccelerationNorth;
    //    var accelerationEast = left.AccelerationEast - right.AccelerationEast;
    //    var accelerationDown = left.AccelerationDown - right.AccelerationDown;

    //    var result = new AccelerationNED(accelerationNorth, accelerationEast, accelerationDown);

    //    return result;
    //}

    //public static AccelerationNED operator *(AccelerationNED left, double right)
    //{
    //    var accelerationNorth = left.AccelerationNorth * right;
    //    var accelerationEast = left.AccelerationEast * right;
    //    var accelerationDown = left.AccelerationDown * right;

    //    var result = new AccelerationNED(accelerationNorth, accelerationEast, accelerationDown);

    //    return result;
    //}

    //public static AccelerationNED operator *(double left, AccelerationNED right)
    //{
    //    var accelerationNorth = left * right.AccelerationNorth;
    //    var accelerationEast = left * right.AccelerationEast;
    //    var accelerationDown = left * right.AccelerationDown;

    //    var result = new AccelerationNED(accelerationNorth, accelerationEast, accelerationDown);

    //    return result;
    //}

    public static VelocityNED operator *(AccelerationNED left, DeltaTime right)
    {
        var time = right.Time;

        var accelerationNorth = left.AccelerationNorth * time;
        var accelerationEast = left.AccelerationEast * time;
        var accelerationDown = left.AccelerationDown * time;

        var result = new VelocityNED(accelerationNorth, accelerationEast, accelerationDown);

        return result;
    }

    //public static AccelerationNED operator /(AccelerationNED left, double right)
    //{
    //    var accelerationNorth = left.AccelerationNorth / right;
    //    var accelerationEast = left.AccelerationEast / right;
    //    var accelerationDown = left.AccelerationDown / right;

    //    var result = new AccelerationNED(accelerationNorth, accelerationEast, accelerationDown);

    //    return result;
    //}

    //public static AccelerationNED operator /(double left, AccelerationNED right)
    //{
    //    var accelerationNorth = left / right.AccelerationNorth;
    //    var accelerationEast = left / right.AccelerationEast;
    //    var accelerationDown = left / right.AccelerationDown;

    //    var result = new AccelerationNED(accelerationNorth, accelerationEast, accelerationDown);

    //    return result;
    //}
}