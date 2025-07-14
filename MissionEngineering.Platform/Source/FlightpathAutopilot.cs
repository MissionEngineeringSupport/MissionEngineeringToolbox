using MissionEngineering.MathLibrary;

namespace MissionEngineering.Platform;

public class FlightpathAutopilot
{
    public FlightpathDemand FlightpathDemand { get; set; }

    public FlightpathData FlightpathData { get; set; }

    public FlightpathDynamics FlightpathDynamics { get; set; }

    public FlightpathAutopilot(FlightpathDynamics flightpathDynamics)
    {
        FlightpathDynamics = flightpathDynamics;

        FlightpathDemand = new FlightpathDemand();
        FlightpathData = new FlightpathData();
    }

    public void Initialise(double time)
    {
        FlightpathDemand.FlightpathDemandFlightpathId = FlightpathData.PlatformId;
        FlightpathDemand.FlightpathDemandTime = time;
        FlightpathDemand.HeadingAngleDemandDeg = FlightpathData.Attitude.HeadingAngleDeg;
        FlightpathDemand.TotalSpeedDemand = FlightpathData.VelocityNED.TotalSpeed;
        FlightpathDemand.AltitudeDemand = FlightpathData.PositionLLA.Altitude;
    }

    public AccelerationTBA GetAccelerationTBA(double time)
    {
        var axialAcceleration = GetAxialAcceleration();
        var lateralAcceleration = GetLateralAcceleration();
        var verticalAcceleration = GetVerticalAcceleration();

        var accelerationTBA = new AccelerationTBA(axialAcceleration, lateralAcceleration, verticalAcceleration);

        return accelerationTBA;
    }

    public void SetFlightpathDemand(FlightpathDemand flightpathDemand)
    {
        FlightpathDemand = flightpathDemand;
    }

    public double GetAxialAcceleration()
    {
        var axialAccelerationMax = FlightpathDynamics.AxialAccelerationMaximum;

        var speed = FlightpathData.VelocityNED.TotalSpeed;

        var speedDemand = FlightpathDemand.TotalSpeedDemand;

        var speedError = speed - speedDemand;

        var axialAcceleration = -speedError * FlightpathDynamics.AxialAccelerationGain;

        axialAcceleration = MathFunctions.LimitWithinRange(-axialAccelerationMax, axialAccelerationMax, axialAcceleration);

        return axialAcceleration;
    }

    public double GetLateralAcceleration()
    {
        var lateralAccelerationMax = FlightpathDynamics.LateralAccelerationMaximum;

        var headingAngleDeg = FlightpathData.Attitude.HeadingAngleDeg;

        var headingAngleDemandDeg = FlightpathDemand.HeadingAngleDemandDeg;

        var headingAngleErrorDeg = MathFunctions.AzimuthDifferenceDeg(headingAngleDeg, headingAngleDemandDeg);

        var lateralAcceleration = -headingAngleErrorDeg * FlightpathDynamics.LateralAccelerationGain;

        lateralAcceleration = MathFunctions.LimitWithinRange(-lateralAccelerationMax, lateralAccelerationMax, lateralAcceleration);

        var bankAngleDemandMaxDeg = FlightpathDynamics.BankAngleMaximumDeg;

        var bankAngleDemandDeg = SetBankAngleFromLateralAcceleration(lateralAcceleration);

        bankAngleDemandDeg = MathFunctions.LimitWithinRange(-bankAngleDemandMaxDeg, bankAngleDemandMaxDeg, bankAngleDemandDeg);

        var bankAngleDeg = FlightpathData.Attitude.BankAngleDeg;

        var bankAngleErrorDeg = bankAngleDeg - bankAngleDemandDeg;

        var bankAngleRateDemandDeg = -bankAngleErrorDeg * FlightpathDynamics.BankAngleGain;

        FlightpathDemand.BankAngleDemandDeg = bankAngleDemandDeg;
        FlightpathDemand.BankAngleRateDemandDeg = bankAngleRateDemandDeg;

        return lateralAcceleration;
    }

    public double GetVerticalAcceleration()
    {
        var pitchAngleMaxDeg = FlightpathDynamics.PitchAngleMaximumDeg;
        var verticalAccelerationMax = FlightpathDynamics.VerticalAccelerationMaximum;

        var altitude = FlightpathData.PositionLLA.Altitude;

        var altitudeDemand = FlightpathDemand.AltitudeDemand;

        var altitudeError = altitude - altitudeDemand;

        var pitchAngleDemandDeg = -altitudeError * FlightpathDynamics.PitchAngleGain;

        pitchAngleDemandDeg = MathFunctions.LimitWithinRange(-pitchAngleMaxDeg, pitchAngleMaxDeg, pitchAngleDemandDeg);

        var pitchAngleDeg = FlightpathData.Attitude.PitchAngleDeg;

        var pitchAngleErorDeg = pitchAngleDeg - pitchAngleDemandDeg;

        var verticalAcceleration = pitchAngleErorDeg * FlightpathDynamics.VerticalAccelerationGain;

        verticalAcceleration = MathFunctions.LimitWithinRange(-verticalAccelerationMax, verticalAccelerationMax, verticalAcceleration);

        FlightpathDemand.PitchAngleDemandDeg = pitchAngleDemandDeg;

        return verticalAcceleration;
    }

    public double SetBankAngleFromLateralAcceleration(double lateralAcceleration)
    {
        if (!FlightpathDynamics.IsUseBankedTurns)
        {
            return 0.0;
        }

        var bankAngleDemandDeg = MathFunctions.CalculateBankAngleDegFromLateralAcceleration(lateralAcceleration);

        return bankAngleDemandDeg;
    }
}