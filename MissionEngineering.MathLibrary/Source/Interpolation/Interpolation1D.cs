namespace MissionEngineering.MathLibrary;

public class Interpolation1D
{
    public double[] X { get; set; }

    public double[] Y { get; set; }

    public Interpolation1D(Vector x, Vector y)
    {
        ArgumentNullException.ThrowIfNull(x, nameof(x));
        ArgumentNullException.ThrowIfNull(y, nameof(y));

        ArgumentOutOfRangeException.ThrowIfNotEqual(x.NumberOfElements, y.NumberOfElements, nameof(x));
        
        X = x.Data;
        Y = y.Data;
    }

    public Interpolation1D(double[] x, double[] y)
    {
        ArgumentNullException.ThrowIfNull(x, nameof(x));
        ArgumentNullException.ThrowIfNull(y, nameof(y));

        ArgumentOutOfRangeException.ThrowIfNotEqual(x.Length, y.Length, nameof(x));   

        X = x;
        Y = y;
    }

    public double Interpolate(double xi)
    {
        var yi = 0.0;

        return yi;
    }

    public double[] Interpolate(double[] xi)
    {
        var yi = new double[xi.Length];

        for (int i = 0; i < xi.Length; i++)
        {
            yi[i] = Interpolate(xi[i]);
        }

        return yi;
    }

    public double[] Interpolate(Span<double> xi)
    {
        var yi = new double[xi.Length];

        for (int i = 0; i < xi.Length; i++)
        {
            yi[i] = Interpolate(xi[i]);
        }

        return yi;
    }

    public Vector Interpolate(Vector xi)
    {
        var yi = new Vector(xi.NumberOfElements);

        for (int i = 0; i < xi.NumberOfElements; i++)
        {
            yi[i] = Interpolate(xi[i]);
        }

        return yi;
    }
}
