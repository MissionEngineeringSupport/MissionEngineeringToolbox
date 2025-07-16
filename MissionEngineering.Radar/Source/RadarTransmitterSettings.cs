using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar;

public class RadarTransmitterSettings
{
    public double TransmitPower { get; set; }

    public double TransmitPower_dBW => TransmitPower.PowerToDecibels();

    public double TransmitPower_dBmW => TransmitPower_dBW + 30.0;
}