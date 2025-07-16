using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar;

public class RadarDetectionModelOutputData
{
    public double SignalPower { get; set; }

    public double SignalPower_dB => SignalPower.PowerToDecibels();

    public double SignalPower_dBm => SignalPower_dB + 30.0;

    public double NoisePower { get; set; }

    public double NoisePower_dB => NoisePower.PowerToDecibels();

    public double NoisePower_dBm => NoisePower_dB + 30.0;

    public double SNR { get; set; }

    public double SNR_dB => SNR.PowerToDecibels();

    public double SNR_dBm => SNR_dB + 30.0;
}