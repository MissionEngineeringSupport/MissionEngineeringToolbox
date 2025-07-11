using static System.Math;

namespace MissionEngineering.MathLibrary;

public static class MappingConversions
{
    public static PositionLLA ConvertPositionNEDToPositionLLA(PositionNED positionNED, PositionLLA positionLLAOrigin)
    {
        var R = PhysicalConstants.EarthRadiusEquatorial;
        var f = PhysicalConstants.EarthFlatteningFactor;

        var u0 = positionLLAOrigin.LatitudeDeg.DegreesToRadians();
        var l0 = positionLLAOrigin.LongitudeDeg.DegreesToRadians();

        var dN = positionNED.PositionNorth;
        var dE = positionNED.PositionEast;

        var numerator = 1 - (2 * f - f * f);
        var denominator = 1 - (2 * f - f * f) * Pow(Sin(u0), 2);

        var Rn = R / Sqrt(denominator);
        var Rm = Rn * numerator / denominator;

        var du = Atan2(1, Rm) * dN;
        var dl = Atan2(1, (Rn * Cos(u0))) * dE;

        var altitudeOrigin = positionLLAOrigin.Altitude;
        var altitude = -positionNED.PositionDown;

        var u = u0 + du;
        var l = l0 + dl;
        var d = altitude - altitudeOrigin;

        var uDeg = u.RadiansToDegrees();
        var lDeg = l.RadiansToDegrees();

        var positionLLA = new PositionLLA()
        {
            LatitudeDeg = uDeg,
            LongitudeDeg = lDeg,
            Altitude = d
        };

        return positionLLA;
    }

    public static PositionNED ConvertPositionLLAToPositionNED(PositionLLA positionLLA, PositionLLA positionLLAOrigin)
    {
        var R = PhysicalConstants.EarthRadiusEquatorial;
        var f = PhysicalConstants.EarthFlatteningFactor;

        var u0 = positionLLAOrigin.LatitudeDeg.DegreesToRadians();
        var l0 = positionLLAOrigin.LongitudeDeg.DegreesToRadians();

        var numerator = 1 - (2 * f - f * f);
        var denominator = 1 - (2 * f - f * f) * Pow(Sin(u0), 2);

        var Rn = R / Sqrt(denominator);
        var Rm = Rn * numerator / denominator;

        var u = positionLLA.LatitudeDeg.DegreesToRadians();
        var l = positionLLA.LongitudeDeg.DegreesToRadians();

        var du = u - u0;
        var dl = l - l0;

        var positionNorth = du / Atan2(1, Rm);
        var positionEast = dl / Atan2(1, (Rn * Cos(u0)));
        var positionDown = -(positionLLA.Altitude - positionLLAOrigin.Altitude);

        var positionNED = new PositionNED()
        {
            PositionNorth = positionNorth,
            PositionEast = positionEast,
            PositionDown = positionDown
        };

        return positionNED;
    }
}