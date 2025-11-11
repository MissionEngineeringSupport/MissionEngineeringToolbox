using Microsoft.VisualStudio.TestTools.UnitTesting;
using MissionEngineering.Radar;

namespace MissionEngineering.Radar.Tests
{
    [TestClass]
    public class RadarDetectionModelDataTests
    {
        [TestMethod]
        public void Constructor_DefaultsAreNull()
        {
            // Arrange & Act
            var modelData = new RadarDetectionModelData();

            // Assert
            Assert.IsNull(modelData.InputData, "InputData should be null by default");
            Assert.IsNull(modelData.OutputData, "OutputData should be null by default");
        }

        [TestMethod]
        public void Properties_CanBeAssignedAndReadBack()
        {
            // Arrange
            var input = new RadarDetectionModelInputData
            {
                RadarSystemSettings = new RadarSystemSettings
                {
                    RadarSystemId = 42,
                    RadarSystemName = "TestSystem"
                }
            };

            var output = new RadarDetectionModelOutputData
            {
                TargetRange_m = 12345.67
            };

            var modelData = new RadarDetectionModelData();

            // Act
            modelData.InputData = input;
            modelData.OutputData = output;

            // Assert
            Assert.IsNotNull(modelData.InputData);
            Assert.IsNotNull(modelData.OutputData);
            Assert.AreEqual(42, modelData.InputData.RadarSystemSettings.RadarSystemId);
            Assert.AreEqual("TestSystem", modelData.InputData.RadarSystemSettings.RadarSystemName);
            Assert.AreEqual(12345.67, modelData.OutputData.TargetRange_m);

            // Mutate inner values and verify the same instances are referenced
            modelData.InputData.RadarSystemSettings.RadarSystemName = "Changed";
            Assert.AreEqual("Changed", input.RadarSystemSettings.RadarSystemName);
        }
    }
}
