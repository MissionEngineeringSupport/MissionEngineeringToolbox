using MissionEngineering.MathLibrary;

namespace MissionEngineering.Platform;

public record FlightpathData
{
    public double FlightpathTime { get; init; }

    public int FlightpathId { get; init; }

    public PositionLLA PositionLLA { get; init; }

    public PositionNED PositionNED { get; init; }

    public VelocityNED VelocityNED { get; init; }

    public AccelerationNED AccelerationNED { get; init; }

    public AccelerationTBA AccelerationTBA { get; init; }

    public Attitude Attitude { get; init; }

    public AttitudeRate AttitudeRate { get; init; }

    public FlightpathData()
    {
        FlightpathId = 0;
        PositionLLA = new PositionLLA();
        PositionNED = new PositionNED();
        VelocityNED = new VelocityNED();
        AccelerationNED = new AccelerationNED();
        AccelerationTBA = new AccelerationTBA();
        Attitude = new Attitude();
        AttitudeRate = new AttitudeRate();
    }
}