using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MissionEngineering.MathLibrary;

namespace MissionEngineering.Sensor;

public class SensorProperties
{
    public int SensorId { get; set; }

    public string SensorName { get; set; }

    public string SensorDescription { get; set; }

    public SensorType SensorType { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsEnabled { get; set; } = true;

    public SensorCoverage SensorCoverage { get; set; }

    public SensorParameters SensorParameters { get; set; }

    public bool IsGenerateRange { get; set; }

    public SensorProperties()
    {
        SensorId = 0;
        SensorName = string.Empty;
        SensorDescription = string.Empty;
        SensorType = SensorType.Undefined;

        SensorCoverage = new SensorCoverage();
    }

}
