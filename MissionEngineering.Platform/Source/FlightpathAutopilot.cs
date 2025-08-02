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
        FlightpathDemand.FlightpathDemandTime_s = time;
        FlightpathDemand.HeadingAngleDemand_deg = FlightpathData.Attitude.HeadingAngle_deg;
        FlightpathDemand.TotalSpeedDemand_ms = FlightpathData.VelocityNED.TotalSpeed_ms;
        FlightpathDemand.AltitudeDemand_m = FlightpathData.PositionLLA.Altitude_m;
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
        var axialAccelerationMax_ms2 = FlightpathDynamics.AxialAccelerationMaximum_ms2;

        var speed_ms = FlightpathData.VelocityNED.TotalSpeed_ms;

        var speedDemand_ms = FlightpathDemand.TotalSpeedDemand_ms;

        var speedError_ms = speed_ms - speedDemand_ms;

        var axialAcceleration_ms2 = -speedError_ms * FlightpathDynamics.AxialAccelerationGain;

        axialAcceleration_ms2 = MathFunctions.LimitWithinRange(-axialAccelerationMax_ms2, axialAccelerationMax_ms2, axialAcceleration_ms2);

        return axialAcceleration_ms2;
    }

    public double GetLateralAcceleration()
    {
        var lateralAccelerationMax_ms2 = FlightpathDynamics.LateralAccelerationMaximum_ms2;

        var headingAngle_deg = FlightpathData.Attitude.HeadingAngle_deg;

        var headingAngleDemand_deg = FlightpathDemand.HeadingAngleDemand_deg;

        var headingAngleError_deg = MathFunctions.AzimuthDifferenceDeg(headingAngle_deg, headingAngleDemand_deg);

        var lateralAcceleration_ms2 = -headingAngleError_deg * FlightpathDynamics.LateralAccelerationGain;

        lateralAcceleration_ms2 = MathFunctions.LimitWithinRange(-lateralAccelerationMax_ms2, lateralAccelerationMax_ms2, lateralAcceleration_ms2);

        var bankAngleDemandMax_deg = FlightpathDynamics.BankAngleMaximum_deg;

        var bankAngleDemand_deg = SetBankAngleFromLateralAcceleration(lateralAcceleration_ms2);

        bankAngleDemand_deg = MathFunctions.LimitWithinRange(-bankAngleDemandMax_deg, bankAngleDemandMax_deg, bankAngleDemand_deg);

        var bankAngle_deg = FlightpathData.Attitude.BankAngle_deg;

        var bankAngleError_deg = bankAngle_deg - bankAngleDemand_deg;

        var bankAngleRateDemand_deg = -bankAngleError_deg * FlightpathDynamics.BankAngleGain;

        FlightpathDemand.BankAngleDemand_deg = bankAngleDemand_deg;
        FlightpathDemand.BankAngleRateDemand_deg = bankAngleRateDemand_deg;

        return lateralAcceleration_ms2;
    }

    public double GetVerticalAcceleration()
    {
        var pitchAngleMax_deg = FlightpathDynamics.PitchAngleMaximum_deg;
        var verticalAccelerationMax_ms2 = FlightpathDynamics.VerticalAccelerationMaximum_ms2;

        var altitude_m = FlightpathData.PositionLLA.Altitude_m;

        var altitudeDemand_m = FlightpathDemand.AltitudeDemand_m;

        var altitudeError_m = altitude_m - altitudeDemand_m;

        var pitchAngleDemand_deg = -altitudeError_m * FlightpathDynamics.PitchAngleGain;

        pitchAngleDemand_deg = MathFunctions.LimitWithinRange(-pitchAngleMax_deg, pitchAngleMax_deg, pitchAngleDemand_deg);

        var pitchAngle_deg = FlightpathData.Attitude.PitchAngle_deg;

        var pitchAngleEror_deg = pitchAngle_deg - pitchAngleDemand_deg;

        var verticalAcceleration_ms2 = pitchAngleEror_deg * FlightpathDynamics.VerticalAccelerationGain;

        verticalAcceleration_ms2 = MathFunctions.LimitWithinRange(-verticalAccelerationMax_ms2, verticalAccelerationMax_ms2, verticalAcceleration_ms2);

        FlightpathDemand.PitchAngleDemand_deg = pitchAngleDemand_deg;

        return verticalAcceleration_ms2;
    }

    public double SetBankAngleFromLateralAcceleration(double lateralAcceleration_ms2)
    {
        if (!FlightpathDynamics.IsUseBankedTurns)
        {
            return 0.0;
        }

        var bankAngleDemand_deg = MathFunctions.CalculateBankAngleDegFromLateralAcceleration(lateralAcceleration_ms2);

        return bankAngleDemand_deg;
    }
}