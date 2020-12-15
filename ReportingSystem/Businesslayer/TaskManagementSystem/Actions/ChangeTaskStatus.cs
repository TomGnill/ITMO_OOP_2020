using ReportingSystem.DataAccesslayer.Task;

namespace ReportingSystem.Businesslayer.TaskManagementSystem.Actions
{
    public class ChangeTaskStatus : IManageTask
    {
        public DataAccesslayer.Task.Task SomeTask;
        public TaskStatus NewStatus;

        public ChangeTaskStatus(DataAccesslayer.Task.Task ourTask, TaskStatus taskStatus)
        {
            SomeTask = ourTask;
            NewStatus = taskStatus;
            SomeActionInTask();
        }

        public DataAccesslayer.Task.Task SomeActionInTask()
        { 
            if (SomeTask.Status != TaskStatus.Resolved)
            {
                SomeTask.Status = NewStatus;
            }
            return SomeTask;
        }
    }
}
