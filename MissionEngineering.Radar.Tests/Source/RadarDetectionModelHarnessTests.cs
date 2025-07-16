namespace MissionEngineering.Radar.Tests;

[TestClass]
public sealed class RadarDetectionModelHarnessTests
{
    [TestMethod]
    public void Update_WithInitialiseCalled_ExpectSuccess()
    {
        // Arrange:

        var rfCenterFrequency = 9.5e9;
        var pulseBandwidth = 50.0e6;

        var inputData = new RadarDetectionModelInputData
        {
            RadarSystemSettings = new RadarSystemSettings()
            {
                RadarSystemId = 1,
                RadarSystemName = "Radar_Test_1",
                SystemLosses_dB = 5.0,
                RFCenterFrequency = rfCenterFrequency,
            },
            RadarTransmitterSettings = new RadarTransmitterSettings()
            {
                TransmitPower = 10000.0
            },
            RadarAntennaSettings = new RadarAntennaSettings()
            {
                AntennaGainTransmit_dB = 34.0,
                AntennaGainReceive_dB = 32.0
            },
            WaveformSettings = new WaveformSettings()
            {
                WaveformId = 1,
                WaveformName = "Waveform_Test_1",
                PulseCenterFrequency = rfCenterFrequency,
                PulseWidth = 1.0e-6,
                PulseBandwidth = pulseBandwidth,
                PulseRepetitionFrequency = 1000.0,
                NumberOfPulses = 10
            },
            RadarReceiverSettings = new RadarReceiverSettings()
            {
                ReceiverBandwidth = pulseBandwidth,
                ReceiverNoiseFigure_dB = 3.0
            },
            RadarTargetSettings = new RadarTargetSettings()
            {
                TargetRange = 1000.0,
                TargetRangeRate = 300.0,
                RadarCrossSection = 10.0
            }
        };

        var radarDetectionModelHarness = new RadarDetectionModelHarness()
        {
            InputData = inputData,
            TargetRangeStart = 1000.0,
            TargetRangeEnd = 10000.0,
            TargetRangeStep = 100.0
        };

        // Act:
        radarDetectionModelHarness.Run();

        // Assert:
        Assert.AreEqual(radarDetectionModelHarness.TargetRanges.NumberOfElements, radarDetectionModelHarness.OutputDataList.Count);
    }
}