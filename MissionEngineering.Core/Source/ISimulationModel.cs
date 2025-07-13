namespace MissionEngineering.Core;

public interface ISimulationModel
{
    void Initialise(double time);

    void Update(double time);

    void Finalise(double time);
}