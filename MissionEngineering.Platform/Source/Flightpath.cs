using MissionEngineering.Core;
using MissionEngineering.MathLibrary;

namespace MissionEngineering.Platform;

public class Flightpath
{
    public PlatformSettings PlatformSettings { get; set; }

    public FlightpathData FlightpathData { get; set; }

    public List<FlightpathData> FlightpathDataList { get; set; }

    public ISimulationClock SimulationClock { get; set; }

    public FlightpathAutopilot FlightpathAutopilot { get; set; }

    public ILLAOrigin LLAOrigin { get; set; }

    public Flightpath(ISimulationClock simulationClock, ILLAOrigin llaOrigin)
    {
        SimulationClock = simulationClock;
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

        var timeStamp = SimulationClock.GetTimeStamp(time);

        FlightpathData = new FlightpathData()
        {
            TimeStamp = timeStamp,
            PlatformId = PlatformSettings.PlatformId,
            PlatformName = PlatformSettings.PlatformName,
            PositionLLA = positionLLA,
            PositionNED = positionNED,
            VelocityNED = velocityNED,
            Attitude = attitude
        };

        var flightpathDynamics = new FlightpathDynamics();

        var flightpathDemand = new FlightpathDemand()
        {
            FlightpathDemandFlightpathId = FlightpathData.PlatformId,
            FlightpathDemandTime = time,
            HeadingAngleDemandDeg = FlightpathData.Attitude.HeadingAngleDeg + 90.0,
            TotalSpeedDemand = FlightpathData.VelocityNED.TotalSpeed + 50,
            AltitudeDemand = FlightpathData.PositionLLA.Altitude + 1000.0,
        };

        FlightpathAutopilot = new FlightpathAutopilot(flightpathDynamics)
        {
            FlightpathData = FlightpathData
        };

        FlightpathAutopilot.SetFlightpathDemand(flightpathDemand);
    }

    public void Update(double time)
    {
        var dt = time - FlightpathData.TimeStamp.SimulationTime;

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

        var timeStamp = SimulationClock.GetTimeStamp(time);

        FlightpathData = new FlightpathData()
        {
            TimeStamp = timeStamp,
            PlatformId = PlatformSettings.PlatformId,
            PlatformName = PlatformSettings.PlatformName,
            PositionLLA = positionLLA,
            PositionNED = positionNED,
            VelocityNED = velocityNED,
            AccelerationNED = accelerationNED,
            AccelerationTBA = accelerationTBA,
            Attitude = attitude,
            AttitudeRate = attitudeRate,
        };

        FlightpathAutopilot.FlightpathData = FlightpathData;

        FlightpathDataList.Add(FlightpathData);
    }

    public void Finalise(double time)
    {
    }

    public AccelerationTBA GetAccelerationTBA(double time)
    {
        var accelerationTBA = FlightpathAutopilot.GetAccelerationTBA(time);

        return accelerationTBA;
    }
}