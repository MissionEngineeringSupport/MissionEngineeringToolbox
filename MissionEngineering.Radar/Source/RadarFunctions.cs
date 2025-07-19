using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar;

public static class RadarFunctions
{
    public static double CalculateSignalPower(RadarDetectionModelInputData inputData)
    {
        var i = inputData;

        var signalPower = CalculateSignalPower(
            i.RadarTransmitterSettings.TransmitPower_W,
            i.RadarSystemSettings.RFCentreWavelength_m,
            i.RadarAntennaSettings.AntennaGainTransmit_dB,
            i.RadarAntennaSettings.AntennaGainReceive_dB,
            i.RadarWaveformSettings.PulseBandwidth_Hz,
            i.RadarWaveformSettings.NumberOfPulses,
            i.RadarSystemSettings.SystemLosses_dB,
            i.RadarTargetSettings.TargetRange_m,
            i.RadarTargetSettings.TargetRangeRate_ms,
            i.RadarTargetSettings.RadarCrossSection_sqm);

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

    public static double CalculateSignalPower(double transmitPower, double rfCenterWavelength, double antennaGainTransmit_dB, double antennaGainReceive_dB, double pulseBandwidth, int numberOfPulses, double systemLosses_dB, double targetRange, double targetRangeRate, double radarCrossSection)
    {
        var antennaGainTransmit = antennaGainTransmit_dB.DecibelsToPower();
        var antennaGainReceive = antennaGainReceive_dB.DecibelsToPower();

        var systemLosses = systemLosses_dB.DecibelsToPower();

        var numerator = transmitPower * antennaGainTransmit * antennaGainReceive * rfCenterWavelength * rfCenterWavelength *  radarCrossSection * numberOfPulses;
        var denominator = Math.Pow(4 * Math.PI, 3) * Math.Pow(targetRange, 4) * systemLosses;

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

    public static double CalculateMaximumUnambiguousRange(double pulseRepetitionFrequency)
    {
        var maximumUnambiguousRange = PhysicalConstants.SpeedOfLight / (2 * pulseRepetitionFrequency);

        return maximumUnambiguousRange;
    }

    public static double CalculateMaximumUnambiguousRangeRate(double rfWavelength, double pulseRepetitionFrequency)
    {
        var maximumUnambiguousRangeRate = rfWavelength * pulseRepetitionFrequency / 2.0;

        return maximumUnambiguousRangeRate;
    }
}