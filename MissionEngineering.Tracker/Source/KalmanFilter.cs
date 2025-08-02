using MissionEngineering.MathLibrary;

namespace MissionEngineering.Tracker;

public abstract class KalmanFilter
{
    public double LastUpdateTime { get; set; }

    public Vector x { get; set; }

    public Matrix P { get; set; }

    public Matrix Phi { get; set; }

    public Matrix Q { get; set; }

    public Vector xPred { get; set; }

    public Matrix PPred { get; set; }

    public Vector z { get; set; }

    public Matrix R { get; set; }

    public Vector zPred { get; set; }

    public Matrix H { get; set; }

    public Vector Innovation { get; set; }

    public Matrix S { get; set; }

    public Vector DeltaX { get; set; }

    public Matrix K { get; set; }

    public int NumberOfStates { get; set; }

    public Vector qSD_Update { get; set; }

    public Vector qSD_Predict { get; set; }

    public void Initialise(double time, Vector x0, Matrix P0)
    {
        LastUpdateTime = time;
        x = x0;
        P = P0;
    }

    public void Update(double time, Vector z, Matrix R, Vector ownshipStates)
    {
        var dt = time - LastUpdateTime;

        Phi = CalculateTransitionMatrix(x, dt);
        Q = CalculateProcessNoiseMatrix(x, dt);

        xPred = Phi * x;
        PPred = Phi * P * Phi.Transpose() + Q;

        xPred = xPred - ownshipStates;

        this.z = z;
        this.R = R;

        zPred = CalculatePredictedMeasurementVector(xPred);
        H = CalculatePredictedMeasurementMatrix(xPred);

        Innovation = z - zPred;
        S = H * PPred * H.Transpose() + R;

        K = PPred * H.Transpose() * S.Inverse();

        DeltaX = K * Innovation;

        x = xPred + DeltaX;
        P = PPred - K * H * PPred;

        x = x + ownshipStates;

        LastUpdateTime = time;
    }
    public (Vector xPred, Matrix PPred) Predict(double time)
    {
        var dt = time - LastUpdateTime;

        var phi = CalculateTransitionMatrix(x, dt);
        var q = CalculateProcessNoiseMatrix(x, dt);

        var xPred = phi * x;
        var PPred = phi * P * phi.Transpose() + q;

        return (xPred, PPred);
    }

    public abstract Matrix CalculateTransitionMatrix(Vector x, double dt);

    public abstract Matrix CalculateProcessNoiseMatrix(Vector x, double dt);

    public abstract Vector CalculatePredictedMeasurementVector(Vector xPred);

    public abstract Matrix CalculatePredictedMeasurementMatrix(Vector xPred);
}