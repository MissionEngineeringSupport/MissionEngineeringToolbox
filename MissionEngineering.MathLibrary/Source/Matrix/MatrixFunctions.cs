namespace MissionEngineering.MathLibrary;

public partial class Matrix
{
    public static Matrix IdentityMatrix(int numberOfRows, int numberOfColumns)
    {
        Matrix x = new Matrix(numberOfRows, numberOfColumns);

        var numberOElements = Math.Min(numberOfRows, numberOfColumns);

        for (int i = 0; i < numberOElements; i++)
        {
            x[i, i] = 1.0;
        }

        return x;
    }
}