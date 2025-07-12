namespace MissionEngineering.Core;

public class ModelTimeStamp
{
    public double SimulationTime { get; set; }

    public DateTime SimulationDateTime { get; set; }

    public DateTime WallClockDateTime { get; set; }

    public ModelTimeStamp()
    {
        WallClockDateTime = DateTime.Now;
    }
}