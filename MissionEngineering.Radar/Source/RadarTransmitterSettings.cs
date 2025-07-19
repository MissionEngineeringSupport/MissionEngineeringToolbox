using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar;

public class RadarTransmitterSettings
{
    public double TransmitPower_W { get; set; }

    public double TransmitPower_dBW => TransmitPower_W.PowerToDecibels();

    public double TransmitPower_dBmW => TransmitPower_dBW + 30.0;
}