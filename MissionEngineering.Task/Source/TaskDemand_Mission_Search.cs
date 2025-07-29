namespace MissionEngineering.Task;

public class TaskDemand_Mission_Search : TaskDemand
{
    public TaskDemand_Mission_Search()
    {
        TaskHeader = new TaskHeader
        {
            TaskId = 0,
            TaskName = "",
            TaskDescription = "",
            TaskType = TaskType.Mission_Search,
            TaskDemandType = TaskDemandType.Create,
            TaskLevel = TaskLevelType.Mission,
            CreationDate = DateTime.UtcNow,
            ModificationDate = DateTime.UtcNow,
            ModificationCount = 0
        };
    }
}