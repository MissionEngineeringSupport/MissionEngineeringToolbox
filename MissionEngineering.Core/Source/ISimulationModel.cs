using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
