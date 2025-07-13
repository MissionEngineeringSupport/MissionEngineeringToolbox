using MissionEngineering.Core;
using MissionEngineering.MathLibrary;

namespace MissionEngineering.Platform.Tests
{
    [TestClass]
    public sealed class PlatformModelTests
    {
        [TestMethod]
        public void Update_WithInitialiseCalled_ExpectSuccess()
        {
            // Arrange:
            var platformSettings = PlatformSettingsFactory.PlatformSettings_Test_1();

            var platformModelState = new PlatformStateData()
            {
                SimulationModelTimeStamp = new SimulationModelTimeStamp(),
                PlatformSettings = platformSettings,
                FlightpathData = new FlightpathData()
            };

            var simulationClock = new SimulationClock(new DateTimeOrigin {  DateTime = new DateTime(2024, 12, 24, 15, 45, 10, 123) } );

            var llaOrigin = new LLAOrigin()
            {
                PositionLLA = new PositionLLA()
                {
                    LatitudeDeg = 55.1,
                    LongitudeDeg = 12.0,
                    Altitude = 0.0
                }
            };

            var platformModel = new PlatformModel(simulationClock, llaOrigin)
            {
                PlatformStateData = platformModelState
            };

            platformModel.Initialise(0.0);

            // Act:
            platformModel.Update(0.0);
            platformModel.Update(1.0);

            //var path = @"C:\Temp\MissionEngineeringToolbox\PlatformData.csv";

            //platformModel.PlatformModelStateList.WriteToCsvFile(path);

            // Assert:
            Assert.IsTrue(platformModel.PlatformStateDataList.Count == 2);
        }
    }
}