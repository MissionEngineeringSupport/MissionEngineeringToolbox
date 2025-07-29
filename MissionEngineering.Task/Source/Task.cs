namespace MissionEngineering.Task;

public class Task
{
    public ITaskDemand TaskDemand { get; set; }

    public ITaskStatus TaskStatus { get; set; }

    public Task(ITaskDemand taskDemand)
    {
        var taskStatus = GetTaskStatus(taskDemand);

        TaskDemand = taskDemand;
        TaskStatus = taskStatus;
    }

    public ITaskStatus GetTaskStatus(ITaskDemand taskDemand)
    {
        var taskStatus = new TaskStatus_Mission_Search()
        {
            TaskHeader = taskDemand.TaskHeader with { TaskStatusType = TaskStatusType.InProgress }
        };

        return taskStatus;
    }
}