using MissionEngineering.MathLibrary;
using MissionEngineering.Sensor;

using static System.Math;

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

        TrackDataSmoothed = new TrackDataSmoothed();
        TrackDataPredicted = new TrackDataPredicted();
        TrackDataSummary = new TrackDataSummary();
    }

    public void InitialiseTrack(SensorReport sensorReport)
    {
        var time = sensorReport.DetectionTime;

        var positionNED = sensorReport.TargetPositionNED;
        var velocityNED = sensorReport.TargetVelocityNED;
        var accelerationNED = new AccelerationNED(0, 0, 0);

        TrackFilter = new KalmanFilterAirTrack_9State_ConstantTurnRate();

        var x = new Vector(positionNED, velocityNED, accelerationNED);

        var p = new Matrix(9, 9)
        {
            [0, 0] = 10000.0,
            [1, 1] = 10000.0,
            [2, 2] = 10000.0,
            [3, 3] = 2500.0,
            [4, 4] = 2500.0,
            [5, 5] = 2500.0,
            [6, 6] = 100.0,
            [7, 7] = 100.0,
            [8, 8] = 100.0
        };

        TrackDataSmoothed.NumberOfUpdates = 1;

        TrackFilter.Initialise(time, x, p);

        UpdateTrackDataSmoothed();
    }

    public void UpdateTrackDataSmoothed()
    {
        var time = TrackFilter.LastUpdateTime;

        var positionNED = new PositionNED(TrackFilter.X[0..3]);
        var velocityNED = new VelocityNED(TrackFilter.X[3..6]);
        var accelerationNED = new AccelerationNED(TrackFilter.X[6..9]);

        var positionLLA = MappingConversions.ConvertPositionNEDToPositionLLA(positionNED, LLAOrigin.PositionLLA);

        var attitude = FrameConversions.GetAttitudeFromVelocityVector(velocityNED);

        TrackDataSmoothed = TrackDataSmoothed with
        {
            LastUpdateTime = time,
            NumberOfUpdates = TrackFilter.NumberOfUpdates,
            PositionLLA = positionLLA,
            PositionNED = positionNED,
            VelocityNED = velocityNED,
            Attitude = attitude,
        };
    }

    public void UpdateTrack(SensorReport sensorReport)
    {
        var time = sensorReport.DetectionTime;

        var targetPositionNED = sensorReport.TargetPositionNED;
        var targetVelocityNED = sensorReport.TargetVelocityNED;

        var z = new Vector(targetPositionNED, targetVelocityNED);

        var r = new Matrix(6, 6)
        {
            [0, 0] = 1000.0,
            [1, 1] = 1000.0,
            [2, 2] = 1000.0,
            [3, 3] = 100.0,
            [4, 4] = 100.0,
            [5, 5] = 100.0
        };

        var ownshipStates = new Vector(9);

        TrackFilter.Update(sensorReport.DetectionTime, z, r, ownshipStates);

        TrackDataSmoothed.NumberOfUpdates++;

        UpdateTrackDataSmoothed();
    }

    public void PredictTrack(double time)
    {
        var (xPred, pPred) = GetPredictedTrack(time);

        var pos = xPred[0..2];

        var positionNED = new PositionNED(xPred[0..3]);
        var velocityNED = new VelocityNED(xPred[3..6]);

        var positionLLA = MappingConversions.ConvertPositionNEDToPositionLLA(positionNED, LLAOrigin.PositionLLA);

        var attitude = FrameConversions.GetAttitudeFromVelocityVector(velocityNED);

        var positionNorthErrorSd = Sqrt(pPred[0, 0]);
        var positionEastErrorSd = Sqrt(pPred[1, 1]);
        var positionDownErrorSd = Sqrt(pPred[2, 2]);

        var positionErrorNEDSd = new PositionNED(positionNorthErrorSd, positionEastErrorSd, positionDownErrorSd);

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
            PositionCovarianceSdNED = positionErrorNEDSd,
        };
    }

    public (Vector xPred, Matrix PPred) GetPredictedTrack(double time)
    {
        var (xPred, PPred) = TrackFilter.Predict(time);

        return (xPred, PPred);
    }
}