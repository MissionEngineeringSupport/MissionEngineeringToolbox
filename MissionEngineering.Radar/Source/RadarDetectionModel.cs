namespace MissionEngineering.Radar;

public class RadarDetectionModel
{
    public RadarDetectionModelInputData InputData { get; set; }

    public RadarDetectionModelOutputData OutputData { get; set; }

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
            SignalPower = signalPower,
            NoisePower = noisePower,
            SNR = snr
        };
    }

    private double GenerateSignalPower()
    {
        var signalPower = RadarFunctions.CalculateSignalPower(InputData);

        return signalPower;
    }

    private double GenerateNoisePower()
    {
        var noisePower = RadarFunctions.CalculateNoisePower(InputData);

        return noisePower;
    }
}