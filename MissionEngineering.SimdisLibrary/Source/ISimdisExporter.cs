﻿using MissionEngineering.Simulation;
using System.Text;

namespace MissionEngineering.SimdisLibrary;

public interface ISimdisExporter
{
    SimulationData SimulationData { get; set; }

    StringBuilder SimdisData { get; set; }

    void GenerateSimdisData();

    void WriteSimdisData();
}