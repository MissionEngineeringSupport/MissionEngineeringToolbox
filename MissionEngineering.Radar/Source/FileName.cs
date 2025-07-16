namespace MissionEngineering.Radar;

public static class RadarDetectionModelInputDataFactory
{
    public static RadarDetectionModelInputData RadarDetectionModelInputData_Test_1()
    {
        var rfCenterFrequency = 9.5e9;
        var pulseBandwidth = 5.0e6;

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
                TransmitPower = 8000.0
            },
            RadarAntennaSettings = new RadarAntennaSettings()
            {
                AntennaGainTransmit_dB = 35.0,
                AntennaGainReceive_dB = 32.0
            },
            WaveformSettings = new WaveformSettings()
            {
                WaveformId = 1,
                WaveformName = "Waveform_Test_1",
                PulseCenterFrequency = rfCenterFrequency,
                PulseWidth = 1.0e-6,
                PulseBandwidth = pulseBandwidth,
                PulseRepetitionFrequency = 150000.0,
                NumberOfPulses = 1024
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

        return inputData;
    }
}