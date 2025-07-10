namespace MissionEngineering.MathLibrary;

public class Vector
{
    public double[] Data { get; set; }

    public int NumberOfElements => Data.Length;

    public Vector()
    {
        Data = Array.Empty<double>();
    }       

    public Vector(int numberOfElements)
    {
        Data = new double[numberOfElements];
    }

    public Vector(double[] data)
    {
        Data = data;
    }

     public double this[int index]
    {
        get => Data[index];
        set => Data[index] = value;
    }
}
