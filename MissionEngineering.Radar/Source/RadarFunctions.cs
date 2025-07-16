using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar;

public static class RadarFunctions
{
    public static double CalculateSignalPower(RadarDetectionModelInputData inputData)
    {
        var i = inputData;

        var signalPower = CalculateSignalPower(
            i.RadarTransmitterSettings.TransmitPower,
            i.RadarSystemSettings.RFCentreWavelength,
            i.RadarAntennaSettings.AntennaGainTransmit_dB,
            i.RadarAntennaSettings.AntennaGainReceive_dB,
            i.WaveformSettings.PulseWidth,
            i.WaveformSettings.PulseBandwidth,
            i.WaveformSettings.NumberOfPulses,
            i.RadarSystemSettings.SystemLosses_dB,
            i.RadarTargetSettings.TargetRange,
            i.RadarTargetSettings.TargetRangeRate,
            i.RadarTargetSettings.RadarCrossSection);

        return signalPower;
    }

    public static double CalculateNoisePower(RadarDetectionModelInputData inputData)
    {
        var i = inputData;

        var signalPower = CalculateNoisePower(
            i.RadarReceiverSettings.ReceiverBandwidth,
            i.RadarReceiverSettings.ReceiverNoiseFigure_dB);

        return signalPower;
    }

    public static double CalculateSignalPower(double transmitPower, double rfCenterWavelength, double antennaGainTransmit_dB, double antennaGainReceive_dB, double pulseWidth, double pulseBandwidth, int numberOfPulses, double systemLosses_dB, double targetRange, double targetRangeRate, double radarCrossSection)
    {
        var antennaGainTransmit = antennaGainTransmit_dB.DecibelsToPower();
        var antennaGainReceive = antennaGainReceive_dB.DecibelsToPower();
        
        var systemLosses = systemLosses_dB.DecibelsToPower();
        
        var numerator = transmitPower * antennaGainTransmit * antennaGainReceive * pulseWidth * numberOfPulses * radarCrossSection;
        var denominator = Math.Pow(4 * Math.PI, 3) * Math.Pow(targetRange, 4) * rfCenterWavelength * rfCenterWavelength * systemLosses;

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
}