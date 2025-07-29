namespace MissionEngineering.Task;

public abstract class TaskStatus : ITaskStatus
{
    public TaskHeader TaskHeader { get; set; }
}