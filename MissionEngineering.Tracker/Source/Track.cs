using MissionEngineering.MathLibrary;
using MissionEngineering.Sensor;

namespace MissionEngineering.Tracker;

public class Track
{
    public TrackDataSmoothed TrackDataSmoothed { get; set; }

    public TrackDataPredicted TrackDataPredicted { get; set; }

    public TrackDataSummary TrackDataSummary { get; set; }

    public ILLAOrigin LLAOrigin { get; set; }

    public IKalmanFilter TrackFilter { get; set; }

    public Track(ILLAOrigin llaOrigin)
    {
        LLAOrigin = llaOrigin;
    }

    public void InitialiseTrack(SensorReport sensorReport)
    {
        var time = sensorReport.DetectionTime;

        var positionNED = sensorReport.TargetPositionNED;
        var velocityNED = sensorReport.TargetVelocityNED;

        var attitude = FrameConversions.GetAttitudeFromVelocityVector(velocityNED);

        //var covarianceSD = sensorReport.TargetPositionErrorNED;
        //var covarianceSD = sensorReport.TargetVelocityErrorNED;

        var positionLLA = MappingConversions.ConvertPositionNEDToPositionLLA(positionNED, LLAOrigin.PositionLLA);
    }

    public void UpdateTrack(SensorReport sensorReport)
    {
    }

    public void PredictTrack(double time)
    {
        var (xPred, PPred) = GetPredictedTrack(time);

        var pos = xPred[0..2];

        var positionNED = new PositionNED(xPred[0..2]);
        var velocityNED = new VelocityNED(xPred[3..5]);

        var attitude = FrameConversions.GetAttitudeFromVelocityVector(velocityNED);

        var positionLLA = MappingConversions.ConvertPositionNEDToPositionLLA(positionNED, LLAOrigin.PositionLLA);

        var ts = TrackDataSmoothed;

        TrackDataPredicted = new TrackDataPredicted
        {
            TrackId = ts.TrackId,
            PredictionTime = time,
            TimeSinceLastUpdate = time - ts.LastUpdateTime,
            LastUpdateTime = ts.LastUpdateTime,
            NumberOfUpdates = ts.NumberOfUpdates,

            PositionLLA = positionLLA,
            PositionNED = positionNED,
            VelocityNED = velocityNED,
            Attitude = attitude,
        };
    }

    public (Vector xPred, Matrix PPred) GetPredictedTrack(double time)
    {
        var (xPred, PPred) = TrackFilter.Predict(time);

        return (xPred, PPred);
    }
}