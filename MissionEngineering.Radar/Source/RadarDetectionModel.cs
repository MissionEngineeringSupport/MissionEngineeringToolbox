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
        var signalPower_W = GenerateSignalPower_W();
        var noisePower_W = GenerateNoisePower_W();
        var atmosphericLoss_dB = GenerateAtmosphericLoss_dB();

        var jammerPower_W = GenerateJammerPower_W();

        var snr = signalPower_W / noisePower_W;

        var sinr = signalPower_W / (jammerPower_W + noisePower_W);

        OutputData = new RadarDetectionModelOutputData
        {
            TargetRange_m = TargetRange_m,
            SignalPower_W = signalPower_W,
            NoisePower_W = noisePower_W,
            JammerPower_W = jammerPower_W,
            AtmosphericLoss_dB = atmosphericLoss_dB,
            SNR = snr,
            SINR = sinr
        };
    }

    private double GenerateSignalPower_W()
    {
        var signalPower_W = RadarFunctions.CalculateSignalPower_W(InputData, TargetRange_m, TargetRangeRate_ms);

        return signalPower_W;
    }

    private double GenerateNoisePower_W()
    {
        var noisePower_W = RadarFunctions.CalculateNoisePower_W(InputData);

        return noisePower_W;
    }

    private double GenerateAtmosphericLoss_dB()
    {
        var atmosphericLoss_dB = RadarFunctions.CalculateAtmosphericLoss_dB(InputData, TargetRange_m);

        return atmosphericLoss_dB;
    }

    private double GenerateJammerPower_W()
    {
        if (!InputData.RadarJammerSettings.IsJammerOn)
        {
            return 1.0e-20;
        }

        var jammerPower_W = RadarFunctions.CalculateJammerPower_W(InputData, TargetRange_m, TargetRangeRate_ms);

        return jammerPower_W;
    }
}