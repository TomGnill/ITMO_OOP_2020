using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Task.Actions
{
    public class ChangeTaskWorker : IManageTask
    {
        public Task SomeTask;
        public Worker.Worker SomeWorker;

        public ChangeTaskWorker(Task task, Worker.Worker newWorker)
        {
            SomeTask = task;
            SomeWorker = newWorker;
            SomeActionInTask();
        }

        public Task SomeActionInTask()
        {
            SomeTask.Responsible = SomeWorker;
            return SomeTask;
        }
    }
}
