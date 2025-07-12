using Microsoft.VisualStudio.TestTools.UnitTesting;
using MissionEngineering.MathLibrary;
using System;

namespace MissionEngineering.MathLibrary.Tests
{
    [TestClass]
    public class MathUtilitiesTests
    {
        [DataTestMethod]
        [DataRow(5.7, 1.0, 6.0)]
        [DataRow(5.2, 1.0, 5.0)]
        [DataRow(5.5, 0.5, 5.5)]
        [DataRow(5.3, 0.5, 5.5)]
        [DataRow(-2.3, 1.0, -2.0)]
        [DataRow(-2.7, 1.0, -3.0)]
        [DataRow(0.0, 0.1, 0.0)]
        public void RoundToStepSize_ReturnsExpectedResult(double value, double stepSize, double expected)
        {
            var result = MathUtilities.RoundToStepSize(value, stepSize);
            Assert.AreEqual(expected, result, 10e-10);
        }

        [DataTestMethod]
        [DataRow(1.0, 0.0)]
        [DataRow(1.0, -1.0)]
        public void RoundToStepSize_InvalidStepSize_Throws(double value, double stepSize)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => MathUtilities.RoundToStepSize(value, stepSize));
        }
    }
}
