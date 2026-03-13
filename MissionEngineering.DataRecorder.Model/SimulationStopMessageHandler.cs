using MissionEngineering.DataRecorder;
using MissionEngineering.Platform;
using MissionEngineering.Simulation;
using Wolverine;

namespace SimulationController;

public class SimulationStopMessageHandler
{
    public void Handle(SimulationStopMessage simulationStopMessage, ILogger<FlightpathData> logger, IMessageContext context, IDataRecorder dataRecorder)
    {
        logger.LogInformation($"Simulation Stop Message Received.");

        dataRecorder.Finalise(0.0);
    }
}