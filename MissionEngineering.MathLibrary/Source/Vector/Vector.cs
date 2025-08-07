﻿using System.Collections;

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

    public Vector(PositionNED positionNED, VelocityNED velocityNED)
    {
        Data = new double[6];

        Data[0] = positionNED.PositionNorth_m;
        Data[1] = positionNED.PositionEast_m;
        Data[2] = positionNED.PositionDown_m;
        Data[3] = velocityNED.VelocityNorth_ms;
        Data[4] = velocityNED.VelocityEast_ms;
        Data[5] = velocityNED.VelocityDown_ms;
    }

    public Vector(PositionNED positionNED, VelocityNED velocityNED, AccelerationNED accelerationNED)
    {
        Data = new double[9];

        Data[0] = positionNED.PositionNorth_m;
        Data[1] = positionNED.PositionEast_m;
        Data[2] = positionNED.PositionDown_m;
        Data[3] = velocityNED.VelocityNorth_ms;
        Data[4] = velocityNED.VelocityEast_ms;
        Data[5] = velocityNED.VelocityDown_ms;
        Data[6] = accelerationNED.AccelerationNorth_ms2;
        Data[7] = accelerationNED.AccelerationEast_ms2;
        Data[8] = accelerationNED.AccelerationDown_ms2;
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

    public double[] this[Range index]
    {
        get => Data[index];
        set => Data = value;
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