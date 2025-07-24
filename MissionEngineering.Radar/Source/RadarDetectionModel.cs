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
        var atmosphericLoss_dB = GenerateAtmosphericLoss_dB();

        var jammerPower = GenerateJammerPower();

        var snr = signalPower / noisePower;

        var sinr = signalPower / (jammerPower + noisePower);

        OutputData = new RadarDetectionModelOutputData
        {
            TargetRange_m = TargetRange_m,
            SignalPower_W = signalPower,
            NoisePower_W = noisePower,
            JammerPower_W = jammerPower,
            AtmosphericLoss_dB = atmosphericLoss_dB,
            SNR = snr,
            SINR = sinr
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

    private double GenerateAtmosphericLoss_dB()
    {
        var atmosphericLoss_dB = RadarFunctions.CalculateAtmosphericLoss_dB(InputData, TargetRange_m);

        return atmosphericLoss_dB;
    }

    private double GenerateJammerPower()
    {
        if (!InputData.RadarJammerSettings.IsJammerOn)
        {
            return 1.0e-20;
        }

        var jammerPower = RadarFunctions.CalculateJammerPower(InputData, TargetRange_m, TargetRangeRate_ms);

        return jammerPower;
    }
}