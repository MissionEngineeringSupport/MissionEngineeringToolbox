namespace MissionEngineering.Core;

public record SimulationModelTimeStamp
{
    public DateTime WallClockDateTime { get; set; }

    public DateTime SimulationDateTime { get; set; }

    public double SimulationTime { get; set; }

    public SimulationModelTimeStamp()
    {
        WallClockDateTime = DateTime.Now;
    }
}