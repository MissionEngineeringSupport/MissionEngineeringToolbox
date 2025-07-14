namespace MissionEngineering.Platform
{
    public interface IFlightpathDemandList
    {
        List<FlightpathDemand> FlightpathDemands { get; set; }

        List<FlightpathDemand> GetFlightpathDemands(double time);
    }
}