using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar;

public class RadarTargetSettings
{
    public double TargetRange { get; set; }

    public double TargetRangeRate { get; set; }

    public double RadarCrossSection { get; set; }

    public double RadarCrossSection_dB => RadarCrossSection.PowerToDecibels();

    public double RadarCrossSection_dBm => RadarCrossSection_dB + 30.0;
}