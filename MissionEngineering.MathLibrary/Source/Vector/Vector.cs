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

    public Vector(double[] data)
    {
        Data = data;
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
}
