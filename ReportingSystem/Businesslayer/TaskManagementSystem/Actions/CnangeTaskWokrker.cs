using ReportingSystem.DataAccesslayer.Worker;

namespace ReportingSystem.Businesslayer.TaskManagementSystem.Actions
{
    public class ChangeTaskWorker : IManageTask
    {
        public DataAccesslayer.Task.Task SomeTask;
        public Worker SomeWorker;

        public ChangeTaskWorker(DataAccesslayer.Task.Task task, Worker newWorker)
        {
            SomeTask = task;
            SomeWorker = newWorker;
            SomeActionInTask();
        }

        public DataAccesslayer.Task.Task SomeActionInTask()
        {
            SomeTask.Responsible = SomeWorker;
            return SomeTask;
        }
    }
}
