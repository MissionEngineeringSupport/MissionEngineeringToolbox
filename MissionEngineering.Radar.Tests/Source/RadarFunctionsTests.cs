using Microsoft.VisualStudio.TestTools.UnitTesting;
using MissionEngineering.Radar;
using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar.Tests
{
    [TestClass]
    public class RadarFunctionsTests
    {
        [TestMethod]
        public void CalculateAtmosphericLoss_dB_TwoWay_CalculatesCorrectly()
        {
            // Arrange
            double atmLossPerKm = 2.0; // dB per km
            double range_m = 500.0; // m

            // Act
            var result = RadarFunctions.CalculateAtmosphericLoss_dB(atmLossPerKm, range_m, isTwoWay: true);

            // 2 dB/km * 0.5 km = 1 dB one-way -> 2 dB two-way
            var expected = 2.0;

            // Assert
            Assert.AreEqual(expected, result, 1e-12);
        }

        [TestMethod]
        public void CalculateRangeFromTimeDelay_TwoWay_CalculatesHalfRoundTrip()
        {
            // Arrange
            double timeDelay_s = 1.0e-6; // 1 microsecond

            // Act
            var result = RadarFunctions.CalculateRangeFromTimeDelay(timeDelay_s, isTwoWay: true);

            // expected = speed of light * timeDelay / 2
            var expected = PhysicalConstants.SpeedOfLight * timeDelay_s / 2.0;

            // Assert
            Assert.AreEqual(expected, result, 1e-9);
        }

        [TestMethod]
        public void CalculateMaximumUnambiguousRange_DelegatesToRangeFromTimeDelay()
        {
            // Arrange
            double pri = 0.002; // seconds

            // Act
            var result = RadarFunctions.CalculateMaximumUnambiguousRange(pri);

            var expected = RadarFunctions.CalculateRangeFromTimeDelay(pri);

            // Assert
            Assert.AreEqual(expected, result, 1e-9);
        }

        [TestMethod]
        public void CalculateMaximumUnambiguousRangeRate_CalculatesCorrectly()
        {
            // Arrange
            double wavelength_m = 0.03; // 3 cm
            double prf = 1000.0; // Hz

            // Act
            var result = RadarFunctions.CalculateMaximumUnambiguousRangeRate(wavelength_m, prf);

            var expected = wavelength_m * prf / 2.0;

            Assert.AreEqual(expected, result, 1e-12);
        }

        [TestMethod]
        public void CalculateNoisePower_ZeroNoiseFigure_UsesBoltzmann()
        {
            // Arrange
            double noiseBandwidth_Hz = 1.0; // 1 Hz
            double noiseFigure_dB = 0.0; // 0 dB -> linear factor 1

            // Act
            var result = RadarFunctions.CalculateNoisePower(noiseBandwidth_Hz, noiseFigure_dB);

            var expected = PhysicalConstants.BoltzmannConstant * PhysicalConstants.SystemReferenceTemperature * noiseBandwidth_Hz * (noiseFigure_dB.DecibelsToPower());

            Assert.AreEqual(expected, result, 1e-30);
        }

        [TestMethod]
        public void CalculateNoisePower_dB_MatchesPowerToDecibels()
        {
            // Arrange
            double noiseBandwidth_Hz = 1000.0;
            double noiseFigure_dB = 3.0;

            // Act
            var result_dB = RadarFunctions.CalculateNoisePower_dB(noiseBandwidth_Hz, noiseFigure_dB);

            var power = RadarFunctions.CalculateNoisePower(noiseBandwidth_Hz, noiseFigure_dB);
            var expected_dB = power.PowerToDecibels();

            Assert.AreEqual(expected_dB, result_dB, 1e-9);
        }

        [TestMethod]
        public void CalculateSignalPower_WithSimpleInputs_ComputesExpectedValue()
        {
            // Arrange - choose simple values so expected can be computed directly
            double transmitPower_W = 1.0;
            double rfCenterWavelength_m = 0.1;
            double antennaGainTransmit_dB = 0.0; // 0 dB -> factor 1
            double antennaGainReceive_dB = 0.0;
            double pulseBandwidth_Hz = 1.0;
            double pulseCompressionRatio = 1.0;
            int numberOfPulses = 1;
            double systemLosses_dB = 0.0; // 0 dB -> factor 1
            double targetRange_m = 1.0;
            double targetRangeRate_m = 0.0;
            double radarCrossSection_m2 = 1.0;
            double atmosphericLoss_dB_per_km = 0.0;

            // Act
            var result = RadarFunctions.CalculateSignalPower(
                transmitPower_W,
                rfCenterWavelength_m,
                antennaGainTransmit_dB,
                antennaGainReceive_dB,
                pulseBandwidth_Hz,
                pulseCompressionRatio,
                numberOfPulses,
                systemLosses_dB,
                targetRange_m,
                targetRangeRate_m,
                radarCrossSection_m2,
                atmosphericLoss_dB_per_km);

            // Compute expected using the same formula from the implementation
            var antennaGainTransmit = antennaGainTransmit_dB.DecibelsToPower();
            var antennaGainReceive = antennaGainReceive_dB.DecibelsToPower();
            var atmosphericLoss_dB = RadarFunctions.CalculateAtmosphericLoss_dB(atmosphericLoss_dB_per_km, targetRange_m, isTwoWay: true);
            var atmosphericLoss = atmosphericLoss_dB.DecibelsToPower();
            var systemLosses = systemLosses_dB.DecibelsToPower();

            var numerator = transmitPower_W * antennaGainTransmit * antennaGainReceive * rfCenterWavelength_m * rfCenterWavelength_m * pulseCompressionRatio * radarCrossSection_m2 * numberOfPulses;
            var denominator = System.Math.Pow(4 * System.Math.PI, 3) * System.Math.Pow(targetRange_m, 4) * systemLosses * atmosphericLoss;
            var expected = numerator / denominator;

            Assert.AreEqual(expected, result, 1e-12);
        }

        [TestMethod]
        public void CalculateJammerPower_WithSimpleInputs_ComputesExpectedValue()
        {
            // Arrange
            double transmitPower_W = 1.0;
            double rfCenterWavelength_m = 0.1;
            double antennaGainTransmit_dB = 0.0;
            double antennaGainReceive_dB = 0.0;
            double pulseBandwidth_Hz = 1.0;
            double systemLosses_dB = 0.0;
            double targetRange_m = 10.0;
            double targetRangeRate_ms = 0.0;
            double atmosphericLoss_dB_per_km = 0.0;

            // Act
            var result = RadarFunctions.CalculateJammerPower(
                transmitPower_W,
                rfCenterWavelength_m,
                antennaGainTransmit_dB,
                antennaGainReceive_dB,
                pulseBandwidth_Hz,
                systemLosses_dB,
                targetRange_m,
                targetRangeRate_ms,
                atmosphericLoss_dB_per_km);

            var antennaGainTransmit = antennaGainTransmit_dB.DecibelsToPower();
            var antennaGainReceive = antennaGainReceive_dB.DecibelsToPower();
            var atmosphericLoss_dB = RadarFunctions.CalculateAtmosphericLoss_dB(atmosphericLoss_dB_per_km, targetRange_m, isTwoWay: false);
            var atmosphericLoss = atmosphericLoss_dB.DecibelsToPower();
            var systemLosses = systemLosses_dB.DecibelsToPower();

            var numerator = transmitPower_W * antennaGainTransmit * antennaGainReceive * rfCenterWavelength_m * rfCenterWavelength_m;
            var denominator = System.Math.Pow(4 * System.Math.PI, 2) * System.Math.Pow(targetRange_m, 2) * systemLosses * atmosphericLoss;
            var expected = numerator / denominator;

            Assert.AreEqual(expected, result, 1e-12);
        }

        [TestMethod]
        public void WrapperMethods_UseInputData_ProduceSameResultsAsParameterOverloads()
        {
            // Arrange - build a full input data object
            var input = new RadarDetectionModelInputData
            {
                RadarTransmitterSettings = new RadarTransmitterSettings { TransmitPower_W = 2.0 },
                RadarSystemSettings = new RadarSystemSettings { RFCentreWavelength_m = 0.05, SystemLosses_dB = 0.0 },
                RadarAntennaSettings = new RadarAntennaSettings { AntennaGainReceive_dB = 0.0, AntennaGainTransmit_dB = 0.0, AntennaSidelobeLevelReceive_dB = -20.0 },
                RadarWaveformSettings = new RadarWaveformSettings { PulseBandwidth_Hz = 1.0, NumberOfPulses = 1 },
                RadarTargetSettings = new RadarTargetSettings { RadarCrossSection_sqm = 1.0 },
                RadarReceiverSettings = new RadarReceiverSettings { ReceiverBandwidth_Hz = 1000.0, ReceiverNoiseFigure_dB = 3.0 },
                RadarJammerSettings = new RadarJammerSettings { JammerPower_W = 1.0, JammerAntennaGainTransmit_dB = 0.0, JammerBandwidth_Hz = 1.0, JammerSystemLosses_dB = 0.0, JammerAntennaType = RadarJammerAntennaType.Mainlobe }
            };

            double targetRange_m = 100.0;
            double targetRangeRate_ms = 0.0;

            // Because RadarEnvironmentSettings was not provided above, the wrapper will attempt to access it - ensure we set it to 0
            input.RadarEnvironmentSettings = new RadarEnvironmentSettings { AtmosphericLoss_dB_per_km = 0.0 };

            // Act
            var wrapperSignal = RadarFunctions.CalculateSignalPower(input, targetRange_m, targetRangeRate_ms);

            var overloadSignal = RadarFunctions.CalculateSignalPower(
                input.RadarTransmitterSettings.TransmitPower_W,
                input.RadarSystemSettings.RFCentreWavelength_m,
                input.RadarAntennaSettings.AntennaGainTransmit_dB,
                input.RadarAntennaSettings.AntennaGainReceive_dB,
                input.RadarWaveformSettings.PulseBandwidth_Hz,
                input.RadarWaveformSettings.PulseCompressionRatio,
                input.RadarWaveformSettings.NumberOfPulses,
                input.RadarSystemSettings.SystemLosses_dB,
                targetRange_m,
                targetRangeRate_ms,
                input.RadarTargetSettings.RadarCrossSection_sqm,
                input.RadarEnvironmentSettings?.AtmosphericLoss_dB_per_km ?? 0.0);

            // Assert both approaches yield the same result
            Assert.AreEqual(overloadSignal, wrapperSignal, 1e-12);

            // Noise wrapper
            var wrapperNoise = RadarFunctions.CalculateNoisePower(input);
            var overloadNoise = RadarFunctions.CalculateNoisePower(input.RadarReceiverSettings.ReceiverBandwidth_Hz, input.RadarReceiverSettings.ReceiverNoiseFigure_dB);

            Assert.AreEqual(overloadNoise, wrapperNoise, 1e-12);

            // Atmospheric loss wrapper
            var wrapperAtm = RadarFunctions.CalculateAtmosphericLoss_dB(input, targetRange_m);
            var overloadAtm = RadarFunctions.CalculateAtmosphericLoss_dB(input.RadarEnvironmentSettings.AtmosphericLoss_dB_per_km, targetRange_m);

            Assert.AreEqual(overloadAtm, wrapperAtm, 1e-12);
        }
    }
}
