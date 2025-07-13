using MissionEngineering.Core;
using MissionEngineering.MathLibrary;

namespace MissionEngineering.Platform;

public class Flightpath
{
    public PlatformSettings PlatformSettings { get; set; }

    public FlightpathData FlightpathData { get; set; }

    public List<FlightpathData> FlightpathDataList { get; set; }

    public ILLAOrigin LLAOrigin { get; set; }

    public Flightpath(ILLAOrigin llaOrigin)
    {
        LLAOrigin = llaOrigin;

        FlightpathData = new FlightpathData();
        FlightpathDataList = new List<FlightpathData>();
    }

    public void Initialise(double time)
    {
        var positionNED = PlatformSettings.PositionNED;
        var velocityNED = PlatformSettings.VelocityNED;

        var attitude = FrameConversions.GetAttitudeFromVelocityVector(velocityNED);

        var positionLLA = positionNED.ToPositionLLA(LLAOrigin.PositionLLA);

        FlightpathData = new FlightpathData()
        {
            FlightpathTime = time,
            FlightpathId = PlatformSettings.PlatformId,
            FlightpathName = PlatformSettings.PlatformName,
            PositionLLA = positionLLA,
            PositionNED = positionNED,
            VelocityNED = velocityNED,
            Attitude = attitude
        };
    }

    public void Update(double time)
    {
        var dt = time - FlightpathData.FlightpathTime;

        var deltaTime = new DeltaTime(dt);

        var bankAngleDegOld = FlightpathData.Attitude.BankAngleDeg;

        var attitude = FrameConversions.GetAttitudeFromVelocityVector(FlightpathData.VelocityNED);

        var accelerationTBA = GetAccelerationTBA(time);
        var accelerationNED = FrameConversions.GetAccelerationNED(accelerationTBA, attitude);

        var velocityNED = FlightpathData.VelocityNED + accelerationNED * deltaTime;
        var positionNED = FlightpathData.PositionNED + velocityNED * deltaTime;
        var positionLLA = positionNED.ToPositionLLA(LLAOrigin.PositionLLA);

        var bankAngleRateDeg = 0.0;

        var bankAngleDeg = bankAngleDegOld + bankAngleRateDeg * dt;

        attitude.BankAngleDeg = bankAngleDeg;

        var attitudeRate = new AttitudeRate(0.0, 0.0, bankAngleRateDeg);

        FlightpathData = new FlightpathData()
        {
            FlightpathTime = time,
            FlightpathId = PlatformSettings.PlatformId,
            FlightpathName = PlatformSettings.PlatformName,
            PositionLLA = positionLLA,
            PositionNED = positionNED,
            VelocityNED = velocityNED,
            AccelerationNED = accelerationNED,
            AccelerationTBA = accelerationTBA,
            Attitude = attitude,
            AttitudeRate = attitudeRate,
        };

        FlightpathDataList.Add(FlightpathData);
    }

    public void Finalise(double time)
    {
    }

    public AccelerationTBA GetAccelerationTBA(double time)
    {
        // Placeholder for actual acceleration calculation logic
        return new AccelerationTBA(0.0, 0.0, 0.0);
    }
}