namespace MissionEngineering.Radar;

public class WaveformSettings
{
    public int WaveformId { get; set; }

    public string WaveformName { get; set; }

    public double PulseCenterFrequency { get; set; }

    public double PulseCenterWavelength { get; set; }

    public double PulseWidth { get; set; }

    public double PulseRepetitionFrequency { get; set; }

    public double PulseRepetitionInterval { get; set; }

    public double PulseBandwidth { get; set; }

    public int NumberOfPulses { get; set; }
}