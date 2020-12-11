using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Task.Actions
{
    public class CnangeTaskWorker : IManageTask
    {
        public Task SomeTask;
        public Worker.Worker SomeWorker;

        public CnangeTaskWorker(Task task, Worker.Worker newWorker)
        {
            SomeTask = task;
            SomeWorker = newWorker;
            Act();
        }

        public Task Act()
        {
            SomeTask.Responsible = SomeWorker;
            return SomeTask;
        }
    }
}
