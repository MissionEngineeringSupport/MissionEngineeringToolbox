using static System.Math;

namespace MissionEngineering.MathLibrary;

public static class FrameConversions
{
    public static AccelerationNED GetAccelerationNED(AccelerationTBA accelerationTBA, Attitude attitude)
    {
        var t = attitude.GetTransformationMatrix_Inverse();

        var accelerationTBAVector = accelerationTBA.ToVector();

        var accelerationNEDVector = t * accelerationTBAVector;

        var accelerationNED = new AccelerationNED(accelerationNEDVector);

        return accelerationNED;
    }

    public static Attitude GetAttitudeFromVelocityVector(VelocityNED velocityNED)
    {
        var headingAngle = Atan2(velocityNED.VelocityEast, velocityNED.VelocityNorth);
        var pitchAngle = -Asin(velocityNED.VelocityDown / velocityNED.TotalSpeed);

        var headingAngleDeg = headingAngle.RadiansToDegrees();
        var pitchAngleDeg = pitchAngle.RadiansToDegrees();
        var bankAngleDeg = 0.0;

        var attitude = new Attitude(headingAngleDeg, pitchAngleDeg, bankAngleDeg);

        return attitude;
    }
}