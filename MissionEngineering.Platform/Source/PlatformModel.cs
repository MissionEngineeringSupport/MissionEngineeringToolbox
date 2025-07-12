using MissionEngineering.Core;
using System.Collections.Generic;

namespace MissionEngineering.Platform;

public class PlatformModel : ISimulationModel
{
    public ISimulationModelInitialState SimulationModelInitialState { get; set; }

    public ISimulationModelState SimulationModelState { get => PlatformModelState; set => PlatformModelState = (PlatformModelState)SimulationModelState; }

    public List<ISimulationModelState> SimulationModelStateList { get => PlatformModelStateList.Cast<ISimulationModelState>().ToList(); set => PlatformModelStateList = SimulationModelStateList.Cast<PlatformModelState>().ToList(); }

    public PlatformModelState PlatformModelState { get; set; }

    public List<PlatformModelState> PlatformModelStateList { get; set; }

    public void Initialise(double time)
    {
        PlatformModelStateList = [];
    }

    public void Update(double time)
    {
        PlatformModelStateList.Add(PlatformModelState);
    }

    public void Finalise(double time)
    {
        throw new NotImplementedException();
    }
}