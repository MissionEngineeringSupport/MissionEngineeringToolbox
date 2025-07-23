namespace MissionEngineering.Radar.Tests;

[TestClass]
public sealed class RadarDetectionModelHarnessTests
{
    [TestMethod]
    public void Update_WithInitialiseCalled_ExpectSuccess()
    {
        // Arrange:
        var inputData = RadarDetectionModelHarnessInputDataFactory.Scenario_1();

        var radarDetectionModelHarness = new RadarDetectionModelHarness()
        {
            InputData = inputData,
        };

        // Act:
        radarDetectionModelHarness.Run();

        // Assert:
        Assert.AreEqual(radarDetectionModelHarness.TargetRanges.NumberOfElements, radarDetectionModelHarness.OutputDataList[0].Count);
    }
}