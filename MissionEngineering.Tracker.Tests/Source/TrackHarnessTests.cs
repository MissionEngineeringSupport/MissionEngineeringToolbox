using MissionEngineering.Core;
using MissionEngineering.MathLibrary;
using MissionEngineering.Platform;
using System.IO.Enumeration;

namespace MissionEngineering.Tracker.Tests
{
    [TestClass]
    public sealed class TrackHarnessTests
    {
        [TestMethod]
        public void Run_WithValidData_ExpectSuccess()
        {
            // Arrange
            var dateTimeOrigin = new DateTimeOrigin()
            { 
                DateTime = new DateTime(2023, 10, 1, 0, 0, 0, DateTimeKind.Utc)
            };

            var simulationClock = new SimulationClock(dateTimeOrigin);

            var llaOrigin = new LLAOrigin
            {
                PositionLLA = new PositionLLA(51.5074, -0.1278, 0)
            };

            var positionNED = new PositionNED(10000, 0, 0);
            var velocityNED = new VelocityNED(-200, 0, 0);
            var positionLLA = MappingConversions.ConvertPositionNEDToPositionLLA(positionNED, llaOrigin.PositionLLA);
            var attitude = FrameConversions.GetAttitudeFromVelocityVector(velocityNED);

            var timeStamp = simulationClock.GetTimeStamp(0.0);

            var flightpathData = new FlightpathData
            {
                TimeStamp = timeStamp,
                PlatformId = 1,
                PlatformName = "TestPlatform",
                PositionLLA = positionLLA,
                PositionNED = positionNED,
                VelocityNED = velocityNED,
                AccelerationNED = new AccelerationNED(0, 0, 0),
                Attitude = attitude,
                AttitudeRate = new AttitudeRate(0, 0, 0)
            };

            var trackHarness = new TrackHarness(llaOrigin)
            {
                StartTime = 0.0,
                EndTime = 20.0,
                UpdateTimeStep = 2.0,
                PredictionTimeStep = 0.1
            };

            trackHarness.FlightpathData = flightpathData;

            // Act
            trackHarness.Run();

            // Analyse
            var fileName = Path.Combine(Environment.CurrentDirectory, "TrackHarnessTestOutput.csv");

            trackHarness.TrackDataPredictedList.WriteToCsvFile(fileName);

            // Assert
            Assert.AreEqual(trackHarness.PredictionTimes.NumberOfElements, trackHarness.TrackDataPredictedList.Count);
        }
    }
}
