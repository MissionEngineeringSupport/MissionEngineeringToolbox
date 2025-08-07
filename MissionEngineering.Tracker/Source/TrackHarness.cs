using MissionEngineering.Core;
using MissionEngineering.MathLibrary;
using MissionEngineering.Platform;
using MissionEngineering.Sensor;
using System.Net.Security;

namespace MissionEngineering.Tracker;

public class TrackHarness : ISimulationModel
{
    public ILLAOrigin LLAOrigin { get; set; }

    public FlightpathData FlightpathData { get; set; }

    public double StartTime { get; set; }

    public double EndTime { get; set; }

    public double UpdateTimeStep { get; set; }

    public double PredictionTimeStep { get; set; }

    public Vector UpdateTimes { get; set; }

    public Vector PredictionTimes { get; set; }

    public List<TrackDataSmoothed> TrackDataSmoothedList { get; set; }

    public List<TrackDataPredicted> TrackDataPredictedList { get; set; }

    public SensorReport SensorReport { get; set; }

    public Track Track { get; set; }

    public TrackHarness(ILLAOrigin llaOrigin)
    {
        LLAOrigin = llaOrigin;
    }

    public void Run()
    {
        UpdateTimes = Vector.LinearlySpacedVector(StartTime, EndTime, UpdateTimeStep);
        PredictionTimes = Vector.LinearlySpacedVector(StartTime, EndTime, PredictionTimeStep);

        var numberOfUpdateSteps = UpdateTimes.NumberOfElements;
        var numberOfPredictSteps = PredictionTimes.NumberOfElements;

        TrackDataSmoothedList = new List<TrackDataSmoothed>(numberOfUpdateSteps);
        TrackDataPredictedList = new List<TrackDataPredicted>(numberOfPredictSteps);

        var numberOfPredictionStepsPerUpdateStep = (int)(UpdateTimeStep / PredictionTimeStep);

        var predictionCount = 0;

        Initialise(StartTime);

        foreach (double time in PredictionTimes)
        {
            var isUpdate = predictionCount == 0;

            if (isUpdate)
            {
                SensorReport = GenerateSensorReport(time);

                Update(time);

                TrackDataSmoothedList.Add(Track.TrackDataSmoothed);
            }

            Predict(time);

            TrackDataPredictedList.Add(Track.TrackDataPredicted);
            predictionCount++;

            if (predictionCount == numberOfPredictionStepsPerUpdateStep)
            {
                predictionCount = 0;
            }
        }
    }

    public void Initialise(double time)
    {
    }

    public void Update(double time)
    {
        SensorReport = GenerateSensorReport(time);

        if (Track is null)
        {
            InitialiseTrack(SensorReport);
        }
        else
        {
            UpdateTrack(SensorReport);
        }
    }

    public void Predict(double time)
    {
        Track.PredictTrack(time);
    }

    public void Finalise(double time)
    {
    }

    public void InitialiseTrack(SensorReport sensorReport)
    { 
        Track = new Track(LLAOrigin);

        Track.TrackDataSmoothed.TrackId = 1001;

        Track.InitialiseTrack(SensorReport);
    }

    public void UpdateTrack(SensorReport sensorReport)
    { 
        Track.UpdateTrack(SensorReport);
    }

    public SensorReport GenerateSensorReport(double time)
    {
        var flightpathData = FlightpathData.Predict(time, LLAOrigin);

        var sensorReport = new SensorReport
        {
            DetectionTime = time,
            TargetPositionLLA = flightpathData.PositionLLA,
            TargetPositionNED = flightpathData.PositionNED,
            TargetVelocityNED = flightpathData.VelocityNED,
            IsTargetRangeValid = true,
            IsTargetRangeRateValid = true, 
            IsTargetAzimuthValid = true,
            IsTargetElevationValid = true,
        };

        return sensorReport;
    }
}