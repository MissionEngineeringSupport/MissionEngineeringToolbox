using MissionEngineering.Core;
using MissionEngineering.MathLibrary;

namespace MissionEngineering.Platform;

public class PlatformModel : ISimulationModel
{
    public PlatformSettings PlatformSettings { get; set; }
    public List<FlightpathData> FlightpathDataList { get; set; }

    public Flightpath Flightpath { get; set; }

    public ILLAOrigin LLAOrigin { get; set; }

    public ISimulationClock SimulationClock { get; set; }

    public PlatformModel(ISimulationClock simulationClock, ILLAOrigin llaOrigin)
    {
        SimulationClock = simulationClock;
        LLAOrigin = llaOrigin;
    }

    public void Initialise(double time)
    {
        Flightpath = new Flightpath(SimulationClock, LLAOrigin)
        {
            LLAOrigin = LLAOrigin,
            PlatformSettings = PlatformSettings
        };

        Flightpath.Initialise(time);

        FlightpathDataList = [];
    }

    public void Update(double time)
    {
        Flightpath.Update(time);

        FlightpathDataList.Add(Flightpath.FlightpathData);
    }

    public void Finalise(double time)
    {
        Flightpath.Finalise(time);
    }
}