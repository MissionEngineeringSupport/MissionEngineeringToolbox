namespace MissionEngineering.Radar;

public class RadarReceiverSettings
{
    public double ReceiverBandwidth_Hz { get; set; }

    public double ReceiverBandwidth_kHz => ReceiverBandwidth_Hz / 1000.0;

    public double ReceiverBandwidth_MHz => ReceiverBandwidth_Hz / 1_000_000.0;

    public double ReceiverNoiseFigure_dB { get; set; }
}