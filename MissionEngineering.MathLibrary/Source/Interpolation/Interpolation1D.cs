using System.Runtime.CompilerServices;

namespace MissionEngineering.MathLibrary;

public class Interpolation1D
{
    public double[] X { get; set; }

    public double[] Y { get; set; }

    public Interpolation1D(double[] x, double[] y)
    {
        if (x == null || y == null)
            throw new ArgumentNullException("X and Y arrays cannot be null.");
        if (x.Length != y.Length)
            throw new ArgumentException("X and Y arrays must have the same length.");

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
