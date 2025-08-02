namespace MissionEngineering.Platform;

public record FlightpathDynamics
{
    public double AxialAccelerationGain { get; init; }

    public double AxialAccelerationMaximum_ms2 { get; init; }

    public double LateralAccelerationGain { get; init; }

    public double LateralAccelerationMaximum_ms2 { get; init; }

    public double VerticalAccelerationGain { get; init; }

    public double VerticalAccelerationMaximum_ms2 { get; init; }

    public double PitchAngleGain { get; init; }

    public double PitchAngleMaximum_deg { get; init; }

    public double BankAngleGain { get; init; }

    public double BankAngleMaximum_deg { get; init; }

    public bool IsUseBankedTurns { get; init; }

    public FlightpathDynamics()
    {
        AxialAccelerationGain = 1.0;
        AxialAccelerationMaximum_ms2 = 20.0;
        LateralAccelerationGain = 10.0;
        LateralAccelerationMaximum_ms2 = 20.0;
        VerticalAccelerationGain = 10.0;
        VerticalAccelerationMaximum_ms2 = 20.0;
        PitchAngleGain = 0.05;
        PitchAngleMaximum_deg = 20.0;
        BankAngleGain = 1.0;
        BankAngleMaximum_deg = 60.0;
        IsUseBankedTurns = true;
    }
}