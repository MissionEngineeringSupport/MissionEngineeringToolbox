using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar;

public static class RadarFunctions
{
    public static double CalculateSignalPower(RadarDetectionModelInputData inputData, double targetRange_m, double targetRangeRate_ms)
    {
        var i = inputData;

        var signalPower = CalculateSignalPower(
            i.RadarTransmitterSettings.TransmitPower_W,
            i.RadarSystemSettings.RFCentreWavelength_m,
            i.RadarAntennaSettings.AntennaGainTransmit_dB,
            i.RadarAntennaSettings.AntennaGainReceive_dB,
            i.RadarWaveformSettings.PulseBandwidth_Hz,
            i.RadarWaveformSettings.PulseCompressionRatio,
            i.RadarWaveformSettings.NumberOfPulses,
            i.RadarSystemSettings.SystemLosses_dB,
            targetRange_m,
            targetRangeRate_ms,
            i.RadarTargetSettings.RadarCrossSection_sqm,
            i.RadarEnvironmentSettings.AtmophericLoss_dB_per_km);

        return signalPower;
    }

    public static double CalculateNoisePower(RadarDetectionModelInputData inputData)
    {
        var i = inputData;

        var signalPower = CalculateNoisePower(
            i.RadarReceiverSettings.ReceiverBandwidth_Hz,
            i.RadarReceiverSettings.ReceiverNoiseFigure_dB);

        return signalPower;
    }

    public static double CalculateAtmosphericLoss_dB(RadarDetectionModelInputData inputData, double targetRange_m)
    {
        var atmosphericLoss_dB = CalculateAtmosphericLoss_dB(inputData.RadarEnvironmentSettings.AtmophericLoss_dB_per_km, targetRange_m);

        return atmosphericLoss_dB;
    }

    public static double CalculateSignalPower(double transmitPower, double rfCenterWavelength, double antennaGainTransmit_dB, double antennaGainReceive_dB, double pulseBandwidth, double pulseCompressionRatio, int numberOfPulses, double systemLosses_dB, double targetRange, double targetRangeRate, double radarCrossSection, double atmophericLoss_dB_per_km)
    {
        var antennaGainTransmit = antennaGainTransmit_dB.DecibelsToPower();
        var antennaGainReceive = antennaGainReceive_dB.DecibelsToPower();

        var atmosphericLoss_dB = atmophericLoss_dB_per_km * 2.0 * targetRange / 1000.0;
        var atmosphericLoss = atmosphericLoss_dB.DecibelsToPower();

        var systemLosses = systemLosses_dB.DecibelsToPower();

        var numerator = transmitPower * antennaGainTransmit * antennaGainReceive * rfCenterWavelength * rfCenterWavelength * pulseCompressionRatio * radarCrossSection * numberOfPulses;
        var denominator = Math.Pow(4 * Math.PI, 3) * Math.Pow(targetRange, 4) * systemLosses * atmosphericLoss;

        var signalPower = numerator / denominator;

        return signalPower;
    }

    public static double CalculateNoisePower(double noiseBandwidth, double noiseFigure_dB)
    {
        var noiseFigure = noiseFigure_dB.DecibelsToPower();

        var noisePower = PhysicalConstants.BoltzmannConstant * PhysicalConstants.SystemReferenceTemperature * noiseBandwidth * noiseFigure;

        return noisePower;
    }

    public static double CalculateNoisePower_dB(double noiseBandwidth, double noiseFigure_dB)
    {
        var noisePower = CalculateNoisePower(noiseBandwidth, noiseFigure_dB);

        var noisePower_dB = noisePower.PowerToDecibels();

        return noisePower_dB;
    }

    public static double CalculateRangeTwoWay(double pulseRepetitionInterval)
    {
        var maximumUnambiguousRange = PhysicalConstants.SpeedOfLight * pulseRepetitionInterval / 2.0;

        return maximumUnambiguousRange;
    }

    public static double CalculateMaximumUnambiguousRangeRate(double rfWavelength, double pulseRepetitionFrequency)
    {
        var maximumUnambiguousRangeRate = rfWavelength * pulseRepetitionFrequency / 2.0;

        return maximumUnambiguousRangeRate;
    }

    public static double CalculateAtmosphericLoss_dB(double atmophericLoss_dB_per_km, double targetRange)
    {
        var atmosphericLoss_dB = atmophericLoss_dB_per_km * 2.0 * targetRange / 1000.0;

        return atmosphericLoss_dB;
    }
}