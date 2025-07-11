using static System.Math;

namespace MissionEngineering.MathLibrary;

public static class MathFunctions
{
    public static double ConstrainAngle0To360(double x)
    {
        var result = x;

        if (x > 360.0)
        {
            result -= 360.0;
        }

        if (x < 0.0)
        {
            result += 360.0;
        }

        return result;
    }

    public static double ConstrainAnglePlusMinus180(double x)
    {
        var result = x;

        if (x > 180.0)
        {
            result -= 360.0;
        }

        if (x < -180.0)
        {
            result += 360.0;
        }

        return result;
    }

    public static double LimitWithinRange(double min, double max, double value)
    {
        if (value < min)
        {
            return min;
        }

        if (value > max)
        {
            return max;
        }

        return value;
    }

    public static double AzimuthDifferenceDeg(double a, double b)
    {
        var result = a - b;

        result = ConstrainAnglePlusMinus180(result);

        return result;
    }

    public static double CalculateBankAngleDegFromLateralAcceleration(double lateralAcceleration)
    {
        var lateralAcceleration_g = lateralAcceleration.MetersPerSecondSquaredToG();

        var loadFactor_g = Abs(lateralAcceleration_g) + 1.0;

        var bankAngleDeg = CalculateBankAngleFromLoadFactor(loadFactor_g);

        bankAngleDeg = bankAngleDeg * Sign(lateralAcceleration_g);

        return bankAngleDeg;
    }

    //public static double CalculateLoadFactorFromBankAngleDeg(double bankAngleDeg)
    //{
    //    var bankAngle = bankAngleDeg.DegreesToRadians();

    //    var loadFactor_g = 1 / Cos(bankAngle);

    //    return loadFactor_g;
    //}

    public static double CalculateBankAngleFromLoadFactor(double loadFactor_g)
    {
        var bankAngle = Acos(1 / loadFactor_g);

        var bankAngleDeg = bankAngle.RadiansToDegrees();

        return bankAngleDeg;
    }
}