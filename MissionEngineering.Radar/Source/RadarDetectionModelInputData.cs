namespace MissionEngineering.Radar;

public class RadarDetectionModelInputData
{
    public RadarSystemSettings RadarSystemSettings { get; set; }

    public RadarAntennaSettings RadarAntennaSettings { get; set; }

    public RadarTransmitterSettings RadarTransmitterSettings { get; set; }

    public RadarReceiverSettings RadarReceiverSettings { get; set; }

    public WaveformSettings WaveformSettings { get; set; }

    public RadarTargetSettings RadarTargetSettings { get; set; }
}