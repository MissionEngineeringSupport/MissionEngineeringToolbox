using MissionEngineering.Core;

namespace MissionEngineering.Platform.Tests
{
    [TestClass]
    public sealed class PlatformModelTests
    {
        [TestMethod]
        public void Update_WithInitialiseCalled_ExpectSuccess()
        {
            // Arrange:
            var platformModelState = new PlatformModelState()
            {
                SimulationModelTimeStamp = new SimulationModelTimeStamp(),
                PlatformSettings = new PlatformSettings(),
                FlightpathData = new FlightpathData()
            };
            
            var platformModel = new PlatformModel()
            { 
                PlatformModelState = platformModelState
            };

            platformModel.Initialise(0.0);

            // Act:
            platformModel.Update(0.0);
            platformModel.Update(1.0);

            //var path = @"C:\Temp\MissionEngineeringToolbox\PlatformData.csv";

            //platformModel.PlatformModelStateList.WriteToCsvFile(path);

            // Assert:
            Assert.IsTrue(platformModel.PlatformModelStateList.Count == 2);
         }
    }
}
