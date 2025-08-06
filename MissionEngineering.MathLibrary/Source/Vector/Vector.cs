using System.Collections;

namespace MissionEngineering.MathLibrary;

public partial class Vector
{
    public double[] Data { get; set; }

    public int NumberOfElements => Data.Length;

    public Vector() : this(0)
    {
    }

    public Vector(int numberOfElements)
    {
        Data = new double[numberOfElements];
    }

    public Vector(params double[] data)
    {
        Data = data;
    }

    public Vector(params IEnumerable<double> data)
    {
        Data = data.ToArray();
    }

    public double this[int index]
    {
        get => Data[index];
        set => Data[index] = value;
    }

    public double this[Index index]
    {
        get => Data[index];
        set => Data[index] = value;
    }

    public Vector this[Range index]
    {
        get => new Vector(Data[index]);
        set => Data = value.Data;
    }

    public IEnumerator GetEnumerator()
    {
        return Data.GetEnumerator();
    }

    public Vector Copy()
    {
        var result = new Vector(NumberOfElements);

        Array.Copy(Data, result.Data, NumberOfElements);

        return result;
    }

    public bool Equals(Vector x, double tolerance = 1.0e-9)
    {
        if (this is null)
        {
            return false;
        }

        if (x is null)
        {
            return false;
        }

        if (x.NumberOfElements != NumberOfElements)
        {
            return false;
        }

        var deltaX = 0.0;

        for (int i = 0; i < Data.Length; i++)
        {
            deltaX = Math.Abs(x.Data[i] - Data[i]);

            if (deltaX > tolerance)
            {
                return false;
            }
        }

        return true;
    }
}