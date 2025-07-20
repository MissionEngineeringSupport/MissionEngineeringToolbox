using MissionEngineering.MathLibrary;

namespace MissionEngineering.Radar;

public class RadarWaveformSettings
{
    public int WaveformId { get; set; }

    public string WaveformName { get; set; }

    public double PulseCenterFrequency_Hz { get; set; }

    public double PulseCenterFrequency_kHz => PulseCenterFrequency_Hz / 1000.0;

    public double PulseCenterFrequency_MHz => PulseCenterFrequency_Hz / 1_000_000.0;

    public double PulseCenterFrequency_GHz => PulseCenterFrequency_Hz / 1_000_000_000.0;

    public double PulseCenterWavelength_m { get => PulseCenterFrequency_Hz.FrequencyToWavelength(); set => PulseCenterFrequency_Hz = value.WavelengthToFrequency(); }

    public double PulseCenterWavelength_cm => PulseCenterWavelength_m * 100.0;

    public double PulseWidth_s { get; set; }

    public double PulseWidth_ms => PulseWidth_s * 1000.0;

    public double PulseWidth_us => PulseWidth_s * 1_000_000.0;

    public double PulseRepetitionFrequency_Hz { get; set; }

    public double PulseRepetitionFrequency_kHz => PulseRepetitionFrequency_Hz / 1000.0;

    public double PulseRepetitionInterval_s => 1.0 / PulseRepetitionFrequency_Hz;

    public double PulseRepetitionInterval_ms => PulseRepetitionInterval_s * 1000.0;

    public double PulseRepetitionInterval_us => PulseRepetitionInterval_s * 1_000_000.0;

    public double DutyRatio => PulseWidth_s * PulseRepetitionFrequency_Hz;

    public double DutyRatio_Percent => DutyRatio * 100.0;

    public double PulseBandwidth_Hz { get; set; }

    public double PulseBandwidth_kHz => PulseBandwidth_Hz / 1000.0;

    public double PulseBandwidth_MHz => PulseBandwidth_Hz / 1_000_000.0;

    public double PulseCompressionRatio => PulseWidth_s * PulseBandwidth_Hz;

    public int NumberOfPulses { get; set; }

    public double PulseBurstDuration_s => PulseRepetitionInterval_s * NumberOfPulses;

    public double PulseBurstDuration_ms => PulseBurstDuration_s * 1000.0;

    public double MaximumUnambiguousRange_m => RadarFunctions.CalculateMaximumUnambiguousRange(PulseRepetitionFrequency_Hz);

    public double MaximumUnambiguousRange_km => MaximumUnambiguousRange_m.MetersToKilometers();

    public double MaximumUnambiguousRange_NM => MaximumUnambiguousRange_m.MetersToNauticalMiles();

    public double MaximumUnambiguousRangeRate_ms => RadarFunctions.CalculateMaximumUnambiguousRangeRate(PulseCenterWavelength_m, PulseRepetitionFrequency_Hz);

    public double MaximumUnambiguousRangeRate_kts => MaximumUnambiguousRangeRate_ms.MetersPerSecondToKnots();

    public double PulseWidthUncompressed_m => RadarFunctions.CalculateMaximumUnambiguousRange(PulseWidth_s);

    public double PulseWidthCompressed_m => PulseWidthUncompressed_m / PulseCompressionRatio;

    public double RangeCellWidth_m => PulseWidthCompressed_m;

    public double DopplerCellWidth_ms => MaximumUnambiguousRangeRate_ms / NumberOfPulses;

    public int NumberOfRangeCells => (int)(MaximumUnambiguousRange_m / RangeCellWidth_m);

    public int NumberOfDopplerCells => NumberOfPulses;
}