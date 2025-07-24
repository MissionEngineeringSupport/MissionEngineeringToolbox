using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar;

public class RadarJammerSettings
{
    public bool IsJammerOn { get; set; }

    public RadarJammerType JammerType { get; set; }

    public RadarJammerAntennaType JammerAntennaType { get; set; }

    public double JammerPower_W { get; set; }

    public double JammerPower_dB => JammerPower_W.PowerToDecibels();

    public double JammerPower_dBm => JammerPower_dB + 30.0;

    public double JammerBandwidth_Hz { get; set; }

    public double JammerBandwidth_kHz => JammerBandwidth_Hz / 1000.0;

    public double JammerBandwidth_MHz => JammerBandwidth_Hz / 1_000_000.0;

    public double JammerAntennaGainTransmit_dB { get; set; }

    public double JammerSystemLosses_dB { get; set; }
}