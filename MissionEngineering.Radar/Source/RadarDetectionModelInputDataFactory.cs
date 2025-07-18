﻿namespace MissionEngineering.Radar;

public static class RadarDetectionModelInputDataFactory
{
    public static RadarDetectionModelInputData RadarDetectionModelInputData_Test_1()
    {
        var rfCenterFrequency_Hz = 9.5e9;
        var pulseBandwidth_Hz = 5.0e6;

        var inputData = new RadarDetectionModelInputData
        {
            RadarSystemSettings = new RadarSystemSettings()
            {
                RadarSystemId = 1,
                RadarSystemName = "Radar_Test_1",
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
                AntennaGainReceive_dB = 32.0
            },
            RadarWaveformSettings = new RadarWaveformSettings()
            {
                WaveformId = 1,
                WaveformName = "Waveform_Test_1",
                PulseCenterFrequency_Hz = rfCenterFrequency_Hz,
                PulseWidth_s = 10.0e-6,
                PulseBandwidth_Hz = pulseBandwidth_Hz,
                PulseRepetitionFrequency_Hz = 1500.0,
                NumberOfPulses = 1024
            },
            RadarReceiverSettings = new RadarReceiverSettings()
            {
                ReceiverBandwidth_Hz = pulseBandwidth_Hz,
                ReceiverNoiseFigure_dB = 3.0
            },
            RadarTargetSettings = new RadarTargetSettings()
            {
                TargetRange_m = 1000.0,
                TargetRangeRate_ms = 300.0,
                RadarCrossSection_sqm = 10.0
            }
        };

        return inputData;
    }
}