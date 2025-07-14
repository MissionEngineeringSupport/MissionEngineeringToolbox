namespace MissionEngineering.Platform;

public record FlightpathDynamics
{
    public double AxialAccelerationGain { get; init; }

    public double AxialAccelerationMaximum { get; init; }

    public double LateralAccelerationGain { get; init; }

    public double LateralAccelerationMaximum { get; init; }

    public double VerticalAccelerationGain { get; init; }

    public double VerticalAccelerationMaximum { get; init; }

    public double PitchAngleGain { get; init; }

    public double PitchAngleMaximumDeg { get; init; }

    public double BankAngleGain { get; init; }

    public double BankAngleMaximumDeg { get; init; }

    public bool IsUseBankedTurns { get; init; }

    public FlightpathDynamics()
    {
        AxialAccelerationGain = 1.0;
        AxialAccelerationMaximum = 20.0;
        LateralAccelerationGain = 10.0;
        LateralAccelerationMaximum = 20.0;
        VerticalAccelerationGain = 10.0;
        VerticalAccelerationMaximum = 20.0;
        PitchAngleGain = 0.05;
        PitchAngleMaximumDeg = 20.0;
        BankAngleGain = 1.0;
        BankAngleMaximumDeg = 60.0;
        IsUseBankedTurns = true;
    }
}