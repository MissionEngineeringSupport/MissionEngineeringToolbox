using JasperFx;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MissionEngineering.Core;
using MissionEngineering.MathLibrary;
using MissionEngineering.Platform;
using MissionEngineering.Simulation;
using Wolverine;
using Wolverine.Nats;

using var host = await Host.CreateDefaultBuilder()
    .UseWolverine(opts =>
    {
        opts.ApplicationAssembly = typeof(Program).Assembly;

        // Connect to NATS
        opts.UseNats("nats://localhost:4222").AutoProvision();

        // Publish to a subject
        opts.PublishMessage<FlightpathData>().ToNatsSubject("messages.FlightpathData");

        opts.PublishMessage<SimulationStopMessage>().ToNatsSubject("messages.SimulationStop");

    }).StartAsync();

var bus = host.Services.GetRequiredService<IMessageBus>();

var numberOfMessages = 10;

for  (int i = 0; i < numberOfMessages; i++)
{
    Thread.Sleep(100);

    var flightpathData = new FlightpathData
    {
        TimeStamp = new SimulationModelTimeStamp { SimulationTime = i * 0.1 },
        PlatformId = 1,
        PlatformName = "Platform 1",
        PositionLLA = new PositionLLA { Latitude_deg = 37.7749, Longitude_deg = -122.4194, Altitude_m = 1000 },
        PositionNED = new PositionNED { PositionNorth_m = 0, PositionEast_m = 0, PositionDown_m = -1000 },
        VelocityNED = new VelocityNED { VelocityNorth_ms = 100, VelocityEast_ms = 0, VelocityDown_ms = 0 },
        AccelerationNED = new AccelerationNED { AccelerationNorth_ms2 = 0, AccelerationEast_ms2 = 0, AccelerationDown_ms2 = 0 },
        AccelerationTBA = new AccelerationTBA { AccelerationAxial_ms2 = 0, AccelerationLateral_ms2 = 0.0, AccelerationVertical_ms2 = 0.0},
        Attitude = new Attitude(),
        AttitudeRate = new AttitudeRate()
    };

    await bus.SendAsync(flightpathData);
}

var simulationStopMessage = new SimulationStopMessage
{
    IsStop = true
};  

await bus.SendAsync(simulationStopMessage);