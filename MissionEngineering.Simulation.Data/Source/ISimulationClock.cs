using MissionEngineering.Core;

namespace MissionEngineering.Simulation
{
    public interface ISimulationClock
    {
        IDateTimeOrigin DateTimeOrigin { get; set; }

        SimulationModelTimeStamp GetTimeStamp(double time);
    }
}