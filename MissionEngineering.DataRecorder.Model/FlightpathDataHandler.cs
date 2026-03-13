using MissionEngineering.DataRecorder;
using MissionEngineering.Platform;
using Wolverine;

namespace SimulationController;

public class FlightpathDataHandler
{
    public void Handle(FlightpathData flightpathData, ILogger<FlightpathData> logger, IMessageContext context, IDataRecorder dataRecorder)
    {
        logger.LogInformation($"Saving Flightpath Data");
        logger.LogInformation($"Time : {flightpathData.TimeStamp.SimulationTime}");

        dataRecorder.SimulationData.PlatformDataAll.Add(flightpathData);
    }
}