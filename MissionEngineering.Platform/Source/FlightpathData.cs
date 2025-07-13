using MissionEngineering.Core;
using MissionEngineering.MathLibrary;

namespace MissionEngineering.Platform;

public record FlightpathData : ISimulationModelState
{
    public SimulationModelTimeStamp TimeStamp { get; set; }

    public int PlatformId { get; init; }

    public string PlatformName { get; init; }

    public PositionLLA PositionLLA { get; init; }

    public PositionNED PositionNED { get; init; }

    public VelocityNED VelocityNED { get; init; }

    public AccelerationNED AccelerationNED { get; init; }

    public AccelerationTBA AccelerationTBA { get; init; }

    public Attitude Attitude { get; init; }

    public AttitudeRate AttitudeRate { get; init; }

    public FlightpathData()
    {
        PlatformId = 0;
        PlatformName = string.Empty;
        PositionLLA = new PositionLLA();
        PositionNED = new PositionNED();
        VelocityNED = new VelocityNED();
        AccelerationNED = new AccelerationNED();
        AccelerationTBA = new AccelerationTBA();
        Attitude = new Attitude();
        AttitudeRate = new AttitudeRate();
    }
}