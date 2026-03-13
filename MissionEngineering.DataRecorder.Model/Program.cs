using ImTools;
using JasperFx.Resources;
using Microsoft.EntityFrameworkCore;
using MissionEngineering.DataRecorder;
using MissionEngineering.SimdisLibrary;
using MissionEngineering.Simulation;
using Wolverine;
using Wolverine.Nats;

var builder = WebApplication.CreateBuilder(args);

builder.UseWolverine(opts =>
{
    opts.ApplicationAssembly = typeof(Program).Assembly;

    // Connect to NATS
    opts.UseNats("nats://localhost:4222").AutoProvision();

    // Listen to a subject
    opts.ListenToNatsSubject("messages.FlightpathData").ProcessInline();

    opts.ListenToNatsSubject("messages.SimulationStop").ProcessInline();

});

builder.Services.AddResourceSetupOnStartup();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ModelDb>(opt => opt.UseInMemoryDatabase("ModelDatabase"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSingleton<SimulationSettings, SimulationSettings>();
builder.Services.AddSingleton<SimulationData, SimulationData>();
builder.Services.AddSingleton<IDataRecorder, DataRecorder>();
builder.Services.AddSingleton<ISimdisExporter, SimdisExporter>();

var app = builder.Build();

var simulationData = app.Services.GetRequiredService<SimulationData>();

simulationData.SimulationSettings = SimulationSettingsFactory.SimulationSettings_Test_1_Single();
simulationData.ScenarioSettings = ScenarioSettingsFactory.ScenarioSettings_Test_1();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var modelData = app.MapGroup("/modelData");

modelData.MapGet("/", GetFlightpathDataAll);

await app.RunAsync();

static async Task<IResult> GetFlightpathDataAll(IDataRecorder dataRecorder)
{
    var data = dataRecorder.SimulationData.PlatformDataAll.ToArray();

    return TypedResults.Ok(data);
}