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
            i.RadarEnvironmentSettings.AtmosphericLoss_dB_per_km);

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
        var atmosphericLoss_dB = CalculateAtmosphericLoss_dB(inputData.RadarEnvironmentSettings.AtmosphericLoss_dB_per_km, targetRange_m);

        return atmosphericLoss_dB;
    }

    public static double CalculateJammerPower(RadarDetectionModelInputData inputData, double targetRange_m, double targetRangeRate_ms)
    {
        var antennaGainReceive_dB = inputData.RadarAntennaSettings.AntennaGainReceive_dB;

        if (inputData.RadarJammerSettings.JammerAntennaType == RadarJammerAntennaType.Sidelobe)
        {
            antennaGainReceive_dB = antennaGainReceive_dB + inputData.RadarAntennaSettings.AntennaSidelobeLevelReceive_dB;
        }

        var i = inputData;

        var jammerPower = CalculateJammerPower(
            i.RadarJammerSettings.JammerPower_W,
            i.RadarSystemSettings.RFCentreWavelength_m,
            i.RadarJammerSettings.JammerAntennaGainTransmit_dB,
            antennaGainReceive_dB,
            i.RadarJammerSettings.JammerBandwidth_Hz,
            i.RadarJammerSettings.JammerSystemLosses_dB,
            targetRange_m,
            targetRangeRate_ms,
            i.RadarEnvironmentSettings.AtmosphericLoss_dB_per_km);

        return jammerPower;
    }

    public static double CalculateSignalPower(double transmitPower_W, double rfCenterWavelength_m, double antennaGainTransmit_dB, double antennaGainReceive_dB, double pulseBandwidth_Hz, double pulseCompressionRatio, int numberOfPulses, double systemLosses_dB, double targetRange_m, double targetRangeRate_m, double radarCrossSection_m2, double atmophericLoss_dB_per_km)
    {
        var antennaGainTransmit = antennaGainTransmit_dB.DecibelsToPower();
        var antennaGainReceive = antennaGainReceive_dB.DecibelsToPower();

        var atmosphericLoss_dB = CalculateAtmosphericLoss_dB(atmophericLoss_dB_per_km, targetRange_m, isTwoWay: true);
        var atmosphericLoss = atmosphericLoss_dB.DecibelsToPower();

        var systemLosses = systemLosses_dB.DecibelsToPower();

        var numerator = transmitPower_W * antennaGainTransmit * antennaGainReceive * rfCenterWavelength_m * rfCenterWavelength_m * pulseCompressionRatio * radarCrossSection_m2 * numberOfPulses;
        var denominator = Math.Pow(4 * Math.PI, 3) * Math.Pow(targetRange_m, 4) * systemLosses * atmosphericLoss;

        var signalPower_W = numerator / denominator;

        return signalPower_W;
    }

    public static double CalculateNoisePower(double noiseBandwidth_Hz, double noiseFigure_dB)
    {
        var noiseFigure = noiseFigure_dB.DecibelsToPower();

        var noisePower_W = PhysicalConstants.BoltzmannConstant * PhysicalConstants.SystemReferenceTemperature * noiseBandwidth_Hz * noiseFigure;

        return noisePower_W;
    }

    public static double CalculateNoisePower_dB(double noiseBandwidth_Hz, double noiseFigure_dB)
    {
        var noisePower = CalculateNoisePower(noiseBandwidth_Hz, noiseFigure_dB);

        var noisePower_dB = noisePower.PowerToDecibels();

        return noisePower_dB;
    }

    public static double CalculateJammerPower(double transmitPower_W, double rfCenterWavelength_m, double antennaGainTransmit_dB, double antennaGainReceive_dB, double pulseBandwidth_Hz, double systemLosses_dB, double targetRange_m, double targetRangeRate_ms, double atmophericLoss_dB_per_km)
    {
        var antennaGainTransmit = antennaGainTransmit_dB.DecibelsToPower();
        var antennaGainReceive = antennaGainReceive_dB.DecibelsToPower();

        var atmosphericLoss_dB = CalculateAtmosphericLoss_dB(atmophericLoss_dB_per_km, targetRange_m, isTwoWay: false);
        var atmosphericLoss = atmosphericLoss_dB.DecibelsToPower();

        var systemLosses = systemLosses_dB.DecibelsToPower();

        var numerator = transmitPower_W * antennaGainTransmit * antennaGainReceive * rfCenterWavelength_m * rfCenterWavelength_m;
        var denominator = Math.Pow(4 * Math.PI, 2) * Math.Pow(targetRange_m, 2) * systemLosses * atmosphericLoss;

        var jammerPower_W = numerator / denominator;

        return jammerPower_W;
    }

    public static double CalculateRangeFromTimeDelay(double timeDelay_s, bool isTwoWay = true)
    {
        var range_m = PhysicalConstants.SpeedOfLight * timeDelay_s;

        if (isTwoWay)
        {
            range_m /= 2.0;
        }

        return range_m;
    }

    public static double CalculateMaximumUnambiguousRange(double pulseRepetitionInterval_s)
    {
        var maximumUnambiguousRange_m = CalculateRangeFromTimeDelay(pulseRepetitionInterval_s);

        return maximumUnambiguousRange_m;
    }

    public static double CalculateMaximumUnambiguousRangeRate(double rfWavelength_m, double pulseRepetitionFrequency_Hz)
    {
        var maximumUnambiguousRangeRate_ms = rfWavelength_m * pulseRepetitionFrequency_Hz / 2.0;

        return maximumUnambiguousRangeRate_ms;
    }

    public static double CalculateAtmosphericLoss_dB(double atmophericLoss_dB_per_km, double targetRange_m, bool isTwoWay = true)
    {
        var atmosphericLoss_dB = atmophericLoss_dB_per_km * targetRange_m / 1000.0;

        if (isTwoWay)
        {
            atmosphericLoss_dB *= 2.0;
        }

        return atmosphericLoss_dB;
    }
}