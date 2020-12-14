using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingSystem.Task.Actions
{
    public class CreateTask : IManageTask
    {
        public Task NewTask;

        public CreateTask(string name, Description description)
        {
            NewTask = new Task(name, description);
            SomeActionInTask();
        }

        public Task SomeActionInTask()
        {
            NewTask.Status = TaskStatus.Open;
            return NewTask;
        }
    }
}
