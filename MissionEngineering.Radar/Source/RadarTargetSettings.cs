using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar;

public class RadarTargetSettings
{
    public double RadarCrossSection_sqm { get; set; }

    public double RadarCrossSection_dBsm => RadarCrossSection_sqm.PowerToDecibels();
}