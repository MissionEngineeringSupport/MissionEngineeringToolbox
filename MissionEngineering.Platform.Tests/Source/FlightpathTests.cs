using MissionEngineering.Core;
using MissionEngineering.MathLibrary;
using MissionEngineering.Platform;

namespace MissionEngineering.Platform.Tests;

[TestClass]
public sealed class FlightpathTests
{
    [TestMethod]
    public void GetPredictedFlightpathData_WithZeroDelta_ReturnsSameStateExceptTimestamp()
    {
        // Arrange
        var dateOrigin = new DateTime(2025, 1, 1, 0, 0, 0);
        var simulationClock = new SimulationClock(new DateTimeOrigin { DateTime = dateOrigin });

        var llaOrigin = new LLAOrigin()
        {
            PositionLLA = new PositionLLA(55.0, 12.0, 0.0)
        };

        var flightpath = new Flightpath(simulationClock, llaOrigin);

        var initialTime = 10.0;

        var timeStamp = simulationClock.GetTimeStamp(initialTime);

        var positionNED = new PositionNED(1000.0, 2000.0, -100.0);
        var velocityNED = new VelocityNED(50.0, -20.0, 5.0);
        var accelerationNED = new AccelerationNED(1.0, -0.5, 0.1);

        flightpath.FlightpathData = new FlightpathData()
        {
            TimeStamp = timeStamp,
            PositionNED = positionNED,
            VelocityNED = velocityNED,
            AccelerationNED = accelerationNED
        };

        // Act
        var predicted = flightpath.GetPredictedFlightpathData(initialTime);

        // Assert
        Assert.IsNotNull(predicted);
        Assert.AreEqual(initialTime, predicted.TimeStamp.SimulationTime, 1e-9);

        // With zero prediction interval the velocity and position should be unchanged
        Assert.AreEqual(flightpath.FlightpathData.VelocityNED.VelocityNorth_ms, predicted.VelocityNED.VelocityNorth_ms, 1e-9);
        Assert.AreEqual(flightpath.FlightpathData.VelocityNED.VelocityEast_ms, predicted.VelocityNED.VelocityEast_ms, 1e-9);
        Assert.AreEqual(flightpath.FlightpathData.VelocityNED.VelocityDown_ms, predicted.VelocityNED.VelocityDown_ms, 1e-9);

        Assert.AreEqual(flightpath.FlightpathData.PositionNED.PositionNorth_m, predicted.PositionNED.PositionNorth_m, 1e-9);
        Assert.AreEqual(flightpath.FlightpathData.PositionNED.PositionEast_m, predicted.PositionNED.PositionEast_m, 1e-9);
        Assert.AreEqual(flightpath.FlightpathData.PositionNED.PositionDown_m, predicted.PositionNED.PositionDown_m, 1e-9);
    }

    [TestMethod]
    public void GetPredictedFlightpathData_WithPositiveDelta_ComputesExpectedKinematics()
    {
        // Arrange
        var dateOrigin = new DateTime(2025, 1, 1, 0, 0, 0);
        var simulationClock = new SimulationClock(new DateTimeOrigin { DateTime = dateOrigin });

        var llaOrigin = new LLAOrigin()
        {
            PositionLLA = new PositionLLA(55.0, 12.0, 0.0)
        };

        var flightpath = new Flightpath(simulationClock, llaOrigin);

        var initialTime = 5.0;

        var timeStamp = simulationClock.GetTimeStamp(initialTime);

        var positionNED = new PositionNED(0.0, 0.0, 0.0);
        var velocityNED = new VelocityNED(10.0, 0.0, -2.0);
        var accelerationNED = new AccelerationNED(1.0, 0.5, 0.0);

        flightpath.FlightpathData = new FlightpathData()
        {
            TimeStamp = timeStamp,
            PositionNED = positionNED,
            VelocityNED = velocityNED,
            AccelerationNED = accelerationNED
        };

        var predictTime = initialTime + 2.0; // predict 2 seconds ahead

        // Act
        var predicted = flightpath.GetPredictedFlightpathData(predictTime);

        // Assert
        Assert.IsNotNull(predicted);
        Assert.AreEqual(predictTime, predicted.TimeStamp.SimulationTime, 1e-9);

        var dt = predictTime - initialTime;

        // expected velocity = v + a * dt
        var expectedVNorth = velocityNED.VelocityNorth_ms + accelerationNED.AccelerationNorth_ms2 * dt;
        var expectedVEast = velocityNED.VelocityEast_ms + accelerationNED.AccelerationEast_ms2 * dt;
        var expectedVDown = velocityNED.VelocityDown_ms + accelerationNED.AccelerationDown_ms2 * dt;

        Assert.AreEqual(expectedVNorth, predicted.VelocityNED.VelocityNorth_ms, 1e-9);
        Assert.AreEqual(expectedVEast, predicted.VelocityNED.VelocityEast_ms, 1e-9);
        Assert.AreEqual(expectedVDown, predicted.VelocityNED.VelocityDown_ms, 1e-9);

        // expected position = p + v * dt (note: implementation uses initial velocity for position update)
        var expectedPNorth = positionNED.PositionNorth_m + velocityNED.VelocityNorth_ms * dt;
        var expectedPEast = positionNED.PositionEast_m + velocityNED.VelocityEast_ms * dt;
        var expectedPDown = positionNED.PositionDown_m + velocityNED.VelocityDown_ms * dt;

        Assert.AreEqual(expectedPNorth, predicted.PositionNED.PositionNorth_m, 1e-9);
        Assert.AreEqual(expectedPEast, predicted.PositionNED.PositionEast_m, 1e-9);
        Assert.AreEqual(expectedPDown, predicted.PositionNED.PositionDown_m, 1e-9);
    }
}
