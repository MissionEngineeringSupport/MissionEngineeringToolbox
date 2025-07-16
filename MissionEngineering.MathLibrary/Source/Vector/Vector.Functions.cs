﻿namespace MissionEngineering.MathLibrary;

public partial class Vector
{
    public static Vector LinearlySpacedVector(double start, double end, double step)
    {
        int numberOfElements = (int)Math.Ceiling((end - start) / step) + 1;

        var data = new double[numberOfElements];

        for (int i = 0; i < numberOfElements; i++)
        {
            data[i] = start + i * step;
        }

        return new Vector(data);
    }
}