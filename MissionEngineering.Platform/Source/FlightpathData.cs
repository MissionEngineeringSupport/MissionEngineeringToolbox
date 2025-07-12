using MissionEngineering.MathLibrary;
using System.ComponentModel.DataAnnotations;

namespace MissionEngineering.Platform;

public record FlightpathData
{
    public double SimulationTime { get; init; }

    public int PlatformId { get; init; }

    public PositionLLA PositionLLA { get; init; }

    public PositionNED PositionNED { get; init; }

    public VelocityNED VelocityNED { get; init; }

    public AccelerationNED AccelerationNED { get; init; }

    public AccelerationNED AcceleratioTBA { get; init; }

    public Attitude Attitude { get; init; }

    public AttitudeRate AttitudeRate { get; init; }
}