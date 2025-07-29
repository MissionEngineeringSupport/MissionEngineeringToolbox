namespace MissionEngineering.Task.Tests;

[TestClass]
public sealed class TaskTests
{
    [TestMethod]
    public void CreateTask_WithTaskDemand_ExpectSuccess()
    {
        // Arrange:
        var taskHeader = new TaskHeader
        {
            TaskId = 1001,
            TaskName = "Mission Task 1",
            TaskDescription = "Mission Task 1 Description",
            TaskType = TaskType.Mission_Search,
            TaskDemandType = TaskDemandType.Create,
            TaskLevel = TaskLevelType.Mission,
            CreationDate = DateTime.UtcNow,
            ModificationDate = DateTime.UtcNow,
            ModificationCount = 1
        };

        var taskDemand = new TaskDemand_Mission_Search
        {
            TaskHeader = taskHeader
        };

        // Act:
        var task = new Task(taskDemand);

        // Assert:
        Assert.AreEqual(task.TaskDemand.TaskHeader.TaskId, task.TaskStatus.TaskHeader.TaskId);
    }
}