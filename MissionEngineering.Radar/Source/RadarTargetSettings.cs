using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar;

public class RadarTargetSettings
{
    public double TargetRange_m { get; set; }

    public double TargetRangeRate_ms { get; set; }

    public double RadarCrossSection_sqm { get; set; }

    public double RadarCrossSection_dBsm => RadarCrossSection_sqm.PowerToDecibels();
}