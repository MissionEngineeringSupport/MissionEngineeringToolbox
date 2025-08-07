using MissionEngineering.MathLibrary;

namespace MissionEngineering.Tracker;

public record TrackDataPredicted
{
    public int TrackId { get; set; }

    public double PredictionTime { get; set; }

    public double TimeSinceLastUpdate { get; set; }

    public double LastUpdateTime { get; set; }

    public int NumberOfUpdates { get; set; }

    public int SensorPlatformId { get; set; }

    public int SensorId { get; set; }

    public string SensorType { get; set; }

    public int TargetPlatformId { get; set; }

    public int TargetReportId { get; set; }

    public PositionLLA PositionLLA { get; set; }

    public PositionNED PositionNED { get; set; }

    public VelocityNED VelocityNED { get; set; }

    public Attitude Attitude { get; set; }

    public PositionNED PositionCovarianceSdNED { get; set; }

    public VelocityNED VelocityCovarianceSdNED { get; set; }

    public AccelerationNED AccelerationCovarianceSdNED { get; set; }
}