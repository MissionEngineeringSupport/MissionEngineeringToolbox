using static System.Math;

namespace MissionEngineering.MathLibrary;

public partial class Vector
{
    public double Norm()
    {
        double sum = 0.0;

        for (int i = 0; i < NumberOfElements; i++)
        {
            sum += Data[i] * Data[i];
        }

        return Sqrt(sum);
    }
}