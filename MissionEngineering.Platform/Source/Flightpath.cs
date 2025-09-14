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
        FlightpathDataList = [];
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
            FlightpathDemandTime_s = time,
            HeadingAngleDemand_deg = FlightpathData.Attitude.HeadingAngle_deg + 90.0,
            TotalSpeedDemand_ms = FlightpathData.VelocityNED.TotalSpeed_ms + 50,
            AltitudeDemand_m = FlightpathData.PositionLLA.Altitude_m + 1000.0,
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

        var bankAngleDegOld = FlightpathData.Attitude.BankAngle_deg;

        var attitude = FrameConversions.GetAttitudeFromVelocityVector(FlightpathData.VelocityNED);

        var accelerationTBA = GetAccelerationTBA(time);
        var accelerationNED = FrameConversions.GetAccelerationNED(accelerationTBA, attitude);

        var velocityNED = FlightpathData.VelocityNED + accelerationNED * deltaTime;
        var positionNED = FlightpathData.PositionNED + velocityNED * deltaTime;
        var positionLLA = positionNED.ToPositionLLA(LLAOrigin.PositionLLA);

        var bankAngleRate_deg = 0.0;

        var bankAngle_deg = bankAngleDegOld + bankAngleRate_deg * dt;

        attitude.BankAngle_deg = bankAngle_deg;

        var attitudeRate = new AttitudeRate(0.0, 0.0, bankAngleRate_deg);

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

    public FlightpathData GetPredictedFlightpathData(double time)
    {
        var predictionTime = time - FlightpathData.TimeStamp.SimulationTime;

        var deltaTime = new DeltaTime(predictionTime);

        var velocityNED = FlightpathData.VelocityNED + FlightpathData.AccelerationNED * deltaTime;
        var positionNED = FlightpathData.PositionNED + FlightpathData.VelocityNED * deltaTime;
        var positionLLA = positionNED.ToPositionLLA(LLAOrigin.PositionLLA);

        var flightpathData = FlightpathData with
        {
            TimeStamp = SimulationClock.GetTimeStamp(time),
            PositionLLA = positionLLA,
            PositionNED = positionNED,
            VelocityNED = velocityNED,
        };

        return flightpathData;
    }

    public AccelerationTBA GetAccelerationTBA(double time)
    {
        var accelerationTBA = FlightpathAutopilot.GetAccelerationTBA(time);

        return accelerationTBA;
    }
}