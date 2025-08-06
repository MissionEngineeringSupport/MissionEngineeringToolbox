using MissionEngineering.MathLibrary;

namespace MissionEngineering.Tracker;

public class EPIICKalmanFilterAirTrack_9State_ConstantTurnRate : KalmanFilter
{
    public double Omega { get; set; }

    public EPIICKalmanFilterAirTrack_9State_ConstantTurnRate()
    {
        NumberOfStates = 9;

        QSD_Update = new Vector([10.0, 10.0, 10.0]);
        QSD_Predict = new Vector([10.0, 10.0, 10.0]);

        Omega = 0.001;
    }

    public override void Update(double time, Vector z, Matrix r, Vector ownshipStates)
    {
        Update(time, z, r, ownshipStates);

        CalculateTurnRate();
    }

    public void CalculateTurnRate()
    {
        var velocity = X[3..5];
        var acceleration = X[6..8];

        var turnRate = acceleration.Norm() / velocity.Norm();

        Omega = turnRate;

        Omega = Math.Max(Omega, 0.001);
    }

    public override Matrix CalculatePredictedMeasurementMatrix(Vector xPred)
    {
        throw new NotImplementedException();
    }

    public override Vector CalculatePredictedMeasurementVector(Vector xPred)
    {
        throw new NotImplementedException();
    }

    public override Matrix CalculateProcessNoiseMatrix(Vector x, double dt)
    {
        throw new NotImplementedException();
    }

    public override Matrix CalculateTransitionMatrix(Vector x, double dt)
    {
        throw new NotImplementedException();
    }
}