using MissionEngineering.MathLibrary;

namespace MissionEngineering.Platform;

public record FlightpathData
{
    public double FlightpathTime { get; init; }

    public int FlightpathId { get; init; }

    public string FlightpathName { get; init; }

    public PositionLLA PositionLLA { get; init; }

    public PositionNED PositionNED { get; init; }

    public VelocityNED VelocityNED { get; init; }

    public AccelerationNED AccelerationNED { get; init; }

    public AccelerationTBA AccelerationTBA { get; init; }

    public Attitude Attitude { get; init; }

    public AttitudeRate AttitudeRate { get; init; }

    public FlightpathData()
    {
        FlightpathTime = 0.0;
        FlightpathId = 0;
        FlightpathName = string.Empty;
        PositionLLA = new PositionLLA();
        PositionNED = new PositionNED();
        VelocityNED = new VelocityNED();
        AccelerationNED = new AccelerationNED();
        AccelerationTBA = new AccelerationTBA();
        Attitude = new Attitude();
        AttitudeRate = new AttitudeRate();
    }
}