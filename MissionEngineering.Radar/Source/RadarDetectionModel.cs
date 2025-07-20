namespace MissionEngineering.Radar;

public class RadarDetectionModel
{
    public RadarDetectionModelInputData InputData { get; set; }

    public RadarDetectionModelOutputData OutputData { get; set; }

    public double TargetRange_m { get; set; }

    public double TargetRangeRate_ms { get; set; }

    public RadarDetectionModel()
    {
    }

    public void Run()
    {
        var signalPower = GenerateSignalPower();
        var noisePower = GenerateNoisePower();

        var snr = signalPower / noisePower;

        OutputData = new RadarDetectionModelOutputData
        {
            TargetRange_m = TargetRange_m,
            SignalPower_W = signalPower,
            NoisePower_W = noisePower,
            SNR = snr
        };
    }

    private double GenerateSignalPower()
    {
        var signalPower = RadarFunctions.CalculateSignalPower(InputData, TargetRange_m, TargetRangeRate_ms);

        return signalPower;
    }

    private double GenerateNoisePower()
    {
        var noisePower = RadarFunctions.CalculateNoisePower(InputData);

        return noisePower;
    }
}