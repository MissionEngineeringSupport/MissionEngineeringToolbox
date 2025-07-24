using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar;

public class RadarDetectionModelOutputData
{
    public double TargetRange_m { get; set; }

    public double TargetRange_km => TargetRange_m.MetersToKilometers();

    public double TargetRange_NM => TargetRange_m.MetersToNauticalMiles();

    public double SignalPower_W { get; set; }

    public double SignalPower_dB => SignalPower_W.PowerToDecibels();

    public double SignalPower_dBm => SignalPower_dB + 30.0;

    public double NoisePower_W { get; set; }

    public double NoisePower_dB => NoisePower_W.PowerToDecibels();

    public double NoisePower_dBm => NoisePower_dB + 30.0;

    public double JammerPower_W { get; set; }

    public double JammerPower_dB => JammerPower_W.PowerToDecibels();

    public double JammerPower_dBm => JammerPower_dB + 30.0;

    public double AtmosphericLoss_dB { get; set; }

    public double SNR { get; set; }

    public double SNR_dB => SNR.PowerToDecibels();

    public double SNR_dBm => SNR_dB + 30.0;

    public double SINR { get; set; }

    public double SINR_dB => SINR.PowerToDecibels();

    public double SINR_dBm => SINR_dB + 30.0;
}