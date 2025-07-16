using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar;

public class RadarSystemSettings
{
    public int RadarSystemId { get; set; }

    public string RadarSystemName { get; set; }

    public double RFCenterFrequency { get; set; }

    public double RFCentreWavelength { get => RFCenterFrequency.FrequencyToWavelength(); set => RFCenterFrequency = value.WavelengthToFrequency(); }

    public double SystemLosses_dB { get; set; }
}