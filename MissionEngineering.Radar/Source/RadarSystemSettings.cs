using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar;

public class RadarSystemSettings
{
    public int RadarSystemId { get; set; }

    public string RadarSystemName { get; set; }

    public double RFCenterFrequency_Hz { get; set; }

    public double RFCenterFrequency_kHz => RFCenterFrequency_Hz / 1000.0;

    public double RFCenterFrequency_MHz => RFCenterFrequency_Hz / 1_000_000.0;

    public double RFCenterFrequency_GHz => RFCenterFrequency_Hz / 1_000_000_000.0;

    public double RFCentreWavelength_m { get => RFCenterFrequency_Hz.FrequencyToWavelength(); set => RFCenterFrequency_Hz = value.WavelengthToFrequency(); }

    public double RFCentreWavelength_cm => RFCentreWavelength_m * 100.0;

    public double SystemLosses_dB { get; set; }
}