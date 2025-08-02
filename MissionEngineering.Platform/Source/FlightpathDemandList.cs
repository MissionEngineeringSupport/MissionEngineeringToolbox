namespace MissionEngineering.Platform;

public class FlightpathDemandList : IFlightpathDemandList
{
    public List<FlightpathDemand> FlightpathDemands { get; set; }

    private double currentTime = -1.0;

    public FlightpathDemandList()
    {
        FlightpathDemands = [];
    }

    public List<FlightpathDemand> GetFlightpathDemands(double time)
    {
        var flightpathDemands = FlightpathDemands.Where(s => s.FlightpathDemandTime_s > currentTime && s.FlightpathDemandTime_s <= time).ToList();

        currentTime = time;

        return flightpathDemands;
    }
}