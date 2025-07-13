using MissionEngineering.Core;

namespace MissionEngineering.Platform;

public class PlatformModel : ISimulationModel
{
    public ISimulationModelInitialState SimulationModelInitialState { get; set; }

    public ISimulationModelState SimulationModelState { get => PlatformStateData; set => PlatformStateData = (PlatformStateData)SimulationModelState; }

    public List<ISimulationModelState> SimulationModelStateList { get => PlatformStateDataList.Cast<ISimulationModelState>().ToList(); set => PlatformStateDataList = SimulationModelStateList.Cast<PlatformStateData>().ToList(); }

    public PlatformStateData PlatformStateData { get; set; }

    public List<PlatformStateData> PlatformStateDataList { get; set; }

    public void Initialise(double time)
    {
        PlatformStateDataList = [];
    }

    public void Update(double time)
    {
        PlatformStateDataList.Add(PlatformStateData);
    }

    public void Finalise(double time)
    {
        throw new NotImplementedException();
    }
}