namespace MissionEngineering.Radar;

public static class RadarDetectionModelHarnessInputDataFactory
{
    public static RadarDetectionModelHarnessInputData Scenario_1()
    {
        var test_1 = RadarDetectionModelInputDataFactory.Radar_Test_1();
        var test_2 = RadarDetectionModelInputDataFactory.Radar_Test_2();

        var inputData = new RadarDetectionModelHarnessInputData
        {
            ScenarioName = "Scenario_1",
            InputDataList = [test_1, test_2],
            TargetRangeData = new RadarDetectionModelHarnessTargetRangeData
            {
                TargetRangeStart = 1000.0,
                TargetRangeEnd = 200000.0,
                TargetRangeStep = 1000.0
            }
        };

        return inputData;
    }
}