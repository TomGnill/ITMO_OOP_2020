using ReportingSystem.DataAccesslayer.Task;

namespace ReportingSystem.Businesslayer.TaskManagementSystem.Actions
{
    public class CreateTask : IManageTask
    {
        public DataAccesslayer.Task.Task NewTask;

        public CreateTask(string name, Description description)
        {
            NewTask = new DataAccesslayer.Task.Task(name, description);
            SomeActionInTask();
        }

        public DataAccesslayer.Task.Task SomeActionInTask()
        {
            NewTask.Status = TaskStatus.Open;
            return NewTask;
        }
    }
}
