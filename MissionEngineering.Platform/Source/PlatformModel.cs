using MissionEngineering.Core;
using MissionEngineering.MathLibrary;

namespace MissionEngineering.Platform;

public class PlatformModel : ISimulationModel
{
    public ISimulationModelInitialState SimulationModelInitialState { get; set; }

    public ISimulationModelState SimulationModelState { get => PlatformStateData; set => PlatformStateData = (PlatformStateData)SimulationModelState; }

    public List<ISimulationModelState> SimulationModelStateList { get => PlatformStateDataList.Cast<ISimulationModelState>().ToList(); set => PlatformStateDataList = SimulationModelStateList.Cast<PlatformStateData>().ToList(); }

    public PlatformSettings PlatformSettings { get; set; }

    public PlatformStateData PlatformStateData { get; set; }

    public List<PlatformStateData> PlatformStateDataList { get; set; }

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
        Flightpath = new Flightpath(LLAOrigin)
        {
            LLAOrigin = LLAOrigin,
            PlatformSettings = PlatformSettings
        };

        Flightpath.Initialise(time);

        PlatformStateData = new PlatformStateData()
        {
            SimulationModelTimeStamp = new SimulationModelTimeStamp(),
            PlatformSettings = PlatformSettings,
            FlightpathData = Flightpath.FlightpathData
        };

        PlatformStateDataList = [];
    }

    public void Update(double time)
    {
        Flightpath.Update(time);

        PlatformStateData.FlightpathData = Flightpath.FlightpathData;

        var platformStateData = new PlatformStateData()
        {
            SimulationModelTimeStamp = SimulationClock.GetTimeStamp(time),
            PlatformSettings = PlatformSettings,
            FlightpathData = Flightpath.FlightpathData
        };

        PlatformStateDataList.Add(platformStateData);
    }

    public void Finalise(double time)
    {
        Flightpath.Finalise(time);
    }
}