using MissionEngineering.Core;

namespace MissionEngineering.Radar.Tests;

[TestClass]
public sealed class RadarDetectionModelHarnessTests
{
    [TestMethod]
    public void Update_WithInitialiseCalled_ExpectSuccess()
    {
        // Arrange:
        var inputData = RadarDetectionModelInputDataFactory.RadarDetectionModelInputData_Test_1();

        var radarDetectionModelHarness = new RadarDetectionModelHarness()
        {
            InputData = inputData,
            TargetRangeStart = 10000.0,
            TargetRangeEnd = 200000.0,
            TargetRangeStep = 1000.0
        };

        // Act:
        radarDetectionModelHarness.Run();

        // Assert:
        Assert.AreEqual(radarDetectionModelHarness.TargetRanges.NumberOfElements, radarDetectionModelHarness.OutputDataList.Count);
    }
}