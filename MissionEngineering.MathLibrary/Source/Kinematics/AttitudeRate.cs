namespace MissionEngineering.MathLibrary;

public record AttitudeRate
{
    public double HeadingAngleRateDeg { get; init; }

    public double PitchAngleRateDeg { get; init; }

    public double BankAngleRateDeg { get; init; }

    public AttitudeRate()
    {
    }

    public AttitudeRate(double headingAngleRateDeg, double pitchAngleRateDeg, double bankAngleRateDeg)
    {
        HeadingAngleRateDeg = headingAngleRateDeg;
        PitchAngleRateDeg = pitchAngleRateDeg;
        BankAngleRateDeg = bankAngleRateDeg;
    }
}