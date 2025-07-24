namespace MissionEngineering.Radar;

public static class RadarDetectionModelInputDataFactory
{
    public static RadarDetectionModelInputData Radar_Test_1()
    {
        var rfCenterFrequency_Hz = 9.5e9;
        var pulseBandwidth_Hz = 5.0e6;

        var inputData = new RadarDetectionModelInputData
        {
            RadarSystemSettings = new RadarSystemSettings()
            {
                RadarSystemId = 1,
                RadarSystemName = "Radar_Test_1",
                RadarSystemColor = "blue",
                SystemLosses_dB = 5.0,
                RFCenterFrequency_Hz = rfCenterFrequency_Hz,
            },
            RadarTransmitterSettings = new RadarTransmitterSettings()
            {
                TransmitPower_W = 8000.0
            },
            RadarAntennaSettings = new RadarAntennaSettings()
            {
                AntennaGainTransmit_dB = 35.0,
                AntennaGainReceive_dB = 32.0,
                AntennaSidelobeLevelReceive_dB = -30.0
            },
            RadarWaveformSettings = new RadarWaveformSettings()
            {
                WaveformId = 1,
                WaveformName = "Waveform_Test_1",
                PulseCenterFrequency_Hz = rfCenterFrequency_Hz,
                PulseWidth_s = 1.0e-6,
                PulseBandwidth_Hz = pulseBandwidth_Hz,
                PulseRepetitionFrequency_Hz = 150000.0,
                NumberOfPulses = 1024
            },
            RadarReceiverSettings = new RadarReceiverSettings()
            {
                ReceiverBandwidth_Hz = pulseBandwidth_Hz,
                ReceiverNoiseFigure_dB = 3.0
            },
            RadarTargetSettings = new RadarTargetSettings()
            {
                RadarCrossSection_sqm = 10.0
            },
            RadarJammerSettings = new RadarJammerSettings()
            {
                IsJammerOn = false,
                JammerPower_W = 100.0,
            },
            RadarEnvironmentSettings = new RadarEnvironmentSettings()
            {
                AtmosphericLoss_dB_per_km = 0.0
            }
        };

        return inputData;
    }

    public static RadarDetectionModelInputData Radar_Test_2()
    {
        var inputData = Radar_Test_1();

        inputData.RadarSystemSettings.RadarSystemId = 2;
        inputData.RadarSystemSettings.RadarSystemName = "Radar_Test_2";
        inputData.RadarSystemSettings.RadarSystemColor = "red";

        var radarJammerSettings = new RadarJammerSettings()
        {
            IsJammerOn = true,
            JammerType = RadarJammerType.SelfProtect,
            JammerAntennaType = RadarJammerAntennaType.Sidelobe,
            JammerPower_W = 100.0,
            JammerBandwidth_Hz = 500.0e6,
            JammerAntennaGainTransmit_dB = 15.0,
        };

        inputData.RadarJammerSettings = radarJammerSettings;

        return inputData;
    }
}