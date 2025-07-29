namespace MissionEngineering.Radar.Tests;

[TestClass]
public sealed class RadarDetectionModelHarnessTests
{
    [TestMethod]
    public void Run_WithValidData_ExpectSuccess()
    {
        // Arrange:
        var inputData = RadarDetectionModelHarnessInputDataFactory.Scenario_1();

        var outputFolder = Environment.CurrentDirectory;

        var inputFilePath = Path.Combine(outputFolder, $"{inputData.ScenarioName}_RadarDetectionModelHarness_InputData.json");

        var radarDetectionModelHarness = new RadarDetectionModelHarness()
        {
            InputData = inputData,
            InputFilePath = inputFilePath,
            OutputFolder = outputFolder
        };
     
        // Act:
        radarDetectionModelHarness.Run();

        radarDetectionModelHarness.OutputData();

        // Assert:
        Assert.AreEqual(radarDetectionModelHarness.TargetRanges.NumberOfElements, radarDetectionModelHarness.OutputDataList[0].Count);
    }
}