using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionEngineering.Task;

public record TaskHeader
{
    public int TaskId { get; set; }

    public string TaskName { get; set; }

    public string TaskDescription { get; set; }

    public TaskType TaskType { get; set; }

    public TaskLevelType TaskLevel { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ModificationDate { get; set; }

    public int ModificationCount { get; set; }

    public TaskDemandType TaskDemandType { get; set; }

    public TaskStatusType TaskStatusType { get; set; }
}
