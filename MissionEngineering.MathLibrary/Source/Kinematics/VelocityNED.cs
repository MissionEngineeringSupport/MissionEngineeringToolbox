using static System.Math;

namespace MissionEngineering.MathLibrary;

public record VelocityNED
{
    public double VelocityNorth_ms { get; init; }

    public double VelocityEast_ms { get; init; }

    public double VelocityDown_ms { get; init; }

    public double TotalSpeed_ms => GetTotalSpeed_ms();

    public double GroundSpeed_ms => GetGroundSpeed_ms();

    public double VerticalSpeed_ms => -VelocityDown_ms;

    public VelocityNED()
    {
    }

    public VelocityNED(double velocityNorth_ms, double velocityEast_ms, double velocityDown_ms)
    {
        VelocityNorth_ms = velocityNorth_ms;
        VelocityEast_ms = velocityEast_ms;
        VelocityDown_ms = velocityDown_ms;
    }

    public static VelocityNED operator +(VelocityNED left, VelocityNED right)
    {
        var velocityNorth_ms = left.VelocityNorth_ms + right.VelocityNorth_ms;
        var velocityEast_ms = left.VelocityEast_ms + right.VelocityEast_ms;
        var velocityDown_ms = left.VelocityDown_ms + right.VelocityDown_ms;

        var result = new VelocityNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return result;
    }

    public static VelocityNED operator -(VelocityNED left)
    {
        var velocityNorth_ms = -left.VelocityNorth_ms;
        var velocityEast_ms = -left.VelocityEast_ms;
        var velocityDown_ms = -left.VelocityDown_ms;

        var result = new VelocityNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return result;
    }

    public static VelocityNED operator -(VelocityNED left, VelocityNED right)
    {
        var velocityNorth_ms = left.VelocityNorth_ms - right.VelocityNorth_ms;
        var velocityEast_ms = left.VelocityEast_ms - right.VelocityEast_ms;
        var velocityDown_ms = left.VelocityDown_ms - right.VelocityDown_ms;

        var result = new VelocityNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return result;
    }

    public static VelocityNED operator *(VelocityNED left, double right)
    {
        var velocityNorth_ms = left.VelocityNorth_ms * right;
        var velocityEast_ms = left.VelocityEast_ms * right;
        var velocityDown_ms = left.VelocityDown_ms * right;

        var result = new VelocityNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return result;
    }

    public static VelocityNED operator *(double left, VelocityNED right)
    {
        var velocityNorth_ms = left * right.VelocityNorth_ms;
        var velocityEast_ms = left * right.VelocityEast_ms;
        var velocityDown_ms = left * right.VelocityDown_ms;

        var result = new VelocityNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return result;
    }

    public static PositionNED operator *(VelocityNED left, DeltaTime right)
    {
        var time = right.Time;

        var velocityNorth_ms = left.VelocityNorth_ms * time;
        var velocityEast_ms = left.VelocityEast_ms * time;
        var velocityDown_ms = left.VelocityDown_ms * time;

        var result = new PositionNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return result;
    }

    public static VelocityNED operator /(VelocityNED left, double right)
    {
        var velocityNorth_ms = left.VelocityNorth_ms / right;
        var velocityEast_ms = left.VelocityEast_ms / right;
        var velocityDown_ms = left.VelocityDown_ms / right;

        var result = new VelocityNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return result;
    }

    public static VelocityNED operator /(double left, VelocityNED right)
    {
        var velocityNorth_ms = left / right.VelocityNorth_ms;
        var velocityEast_ms = left / right.VelocityEast_ms;
        var velocityDown_ms = left / right.VelocityDown_ms;

        var result = new VelocityNED(velocityNorth_ms, velocityEast_ms, velocityDown_ms);

        return result;
    }

    public double GetTotalSpeed_ms()
    {
        var result = Sqrt(VelocityNorth_ms * VelocityNorth_ms + VelocityEast_ms * VelocityEast_ms + VelocityDown_ms * VelocityDown_ms);

        return result;
    }

    public double GetGroundSpeed_ms()
    {
        var result = Sqrt(VelocityNorth_ms * VelocityNorth_ms + VelocityEast_ms * VelocityEast_ms);

        return result;
    }
}