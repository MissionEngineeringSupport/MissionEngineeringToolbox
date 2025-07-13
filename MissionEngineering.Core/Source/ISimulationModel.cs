namespace MissionEngineering.Core;

public interface ISimulationModel
{
    ISimulationModelInitialState SimulationModelInitialState { get; set; }

    ISimulationModelState SimulationModelState { get; set; }

    List<ISimulationModelState> SimulationModelStateList { get; set; }

    void Initialise(double time);

    void Update(double time);

    void Finalise(double time);
}